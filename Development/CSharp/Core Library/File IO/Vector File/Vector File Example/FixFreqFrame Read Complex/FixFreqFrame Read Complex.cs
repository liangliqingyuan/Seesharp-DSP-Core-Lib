﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using SeeSharpTools.JXI.FileIO.VectorFile;
using SeeSharpTools.JXI.DSP.Spectrum;
using SeeSharpTools.JY.ArrayUtility;
using JXI.RF.DSP.Spectrum;

namespace FixFreqFrameFileExampleReadComplex
{
    public partial class MainForm : Form
    {

        public MainForm()
        {
            InitializeComponent();
        }

        #region------------------------- 私有成员 -------------------------        
     
        private FixFrequencyFrameFile _iqFrameFile;
        
        /// <summary>
        /// 用于存放每次从文件读取的多通道交织（Channel-Interleaved）的原始I16 IQ数据。
        /// </summary>
        private short[] _shortIQAllChannels;

        /// <summary>
        /// 用于存放每次用于计算频谱的单通道Complex IQ数据。
        /// </summary>
        private Complex[] _complexIQOneChannel;

        /// <summary>
        /// 用于存放单通道IQ数据的频谱。
        /// </summary>
        private double[] _spectrumOneChannel;

        /// <summary>
        /// 用于存放多通道的频谱，在Chart上显示。
        /// </summary>
        private double[,] _spectrumAllChannels;

        /// <summary>
        /// 当前是否已启动连续读取。
        /// </summary>
        private bool _isContinuousReading;

        #endregion

        #region------------------------- GUI事件处理 -------------------------

        private void MainForm_Load(object sender, EventArgs e)
        {
            // 初始化DataGridView，用于显示文件信息。
            _guiFileInfoView.Rows.Add(4);
            _guiFileInfoView.Rows[0].Cells[0].Value = "通道数";
            _guiFileInfoView.Rows[1].Cells[0].Value = "采样率";
            _guiFileInfoView.Rows[2].Cells[0].Value = "数据起始时间";
            _guiFileInfoView.Rows[3].Cells[0].Value = "数据总帧数";

            // 设置控件默认状态。
            _guiStop.Enabled = false;
        }

        private void GuiBrowseFile_Click(object sender, EventArgs e)
        {
            // 如果用户取消了操作，则直接返回。
            if (_guiFileBrowseDialog.ShowDialog() == DialogResult.Cancel) { return; }

            // 先关闭当前已经打开的文件。
            _iqFrameFile?.Close();
            _iqFrameFile = null;

            // 获取选中的文件路径。
            _guiFilePath.Text = _guiFileBrowseDialog.FileName;

            #region------------------------- 检查选中的文件格式有效 -------------------------

            // 以Vector File基类打开文件，检查文件格式。
            var vectorFile = new VectorFile();
            try
            {
                vectorFile.Open(_guiFilePath.Text, FileMode.Open, FileAccess.Read);
                if (vectorFile.Storage.FileFormat != FileFormat.FixFrequencyFrame || vectorFile.Storage.DataType != DataType.ComplexI16)
                {
                    // 本例程只处理FixFrequencyFrame，ComplexI16格式的文件，如果是其它格式，则抛出异常。
                    throw new VectorFileException(ExceptionEnum.DataTypeConflict, "文件不是IQ Frame格式。");
                }
            }
            catch(Exception exception)
            {
                // 文件格式异常，提示用户并直接返回。
                MessageBox.Show(exception.Message + Environment.NewLine + exception.StackTrace);
                _guiReadControlSetting.Enabled = false;
                return;
            }
            finally
            {
                // 总是关闭文件。
                vectorFile?.Close();
            }

            #endregion

            _iqFrameFile = new FixFrequencyFrameFile();
            try
            {
                // 打开文件，获取数据信息：通道数、数据总点数、采样率等。
                _iqFrameFile.Open(_guiFilePath.Text, FileMode.Open, FileAccess.Read);
                int numOfChannels = _iqFrameFile.Storage.NumberOfChannels;
                long numOfFrames = _iqFrameFile.NumberOfFrames;
                long numOfSamplesPerFrame = _iqFrameFile.Frame.Length; 
                double sampleRate = _iqFrameFile.Sampling.SampleRate;

                #region------------------------- 显示数据信息。 -------------------------

                _guiFileInfoView.Rows[0].Cells[1].Value = numOfChannels;
                _guiFileInfoView.Rows[1].Cells[1].Value = (sampleRate / 1E6).ToString("F7").TrimEnd('0').TrimEnd('.') + @" MS/s";
                _guiFileInfoView.Rows[2].Cells[1].Value = _iqFrameFile.Archive.DateTime.ToString("yyyy-MM-dd HH:mm:ss");
                _guiFileInfoView.Rows[3].Cells[1].Value = numOfFrames;

                #endregion

                #region------------------------- 根据通道数分配数组空间，用于存放每次读取的IQ数据和FFT计算后的频谱数据。 -------------------------

                if (numOfFrames <= 0) { throw new VectorFileException(ExceptionEnum.InvalidFile, "文件未包含数据。"); }

                // 分配数组空间。
                _shortIQAllChannels = new short[numOfSamplesPerFrame * 2 * numOfChannels];
                _complexIQOneChannel = new Complex[numOfSamplesPerFrame];
                _spectrumOneChannel = new double[numOfSamplesPerFrame];
                _spectrumAllChannels = new double[numOfChannels, numOfSamplesPerFrame];

                #endregion

                #region------------------------- 根据数据总时长设置TrackBar控件属性。 -------------------------

                // 简化实现：TrackBar的最大值为数据的总帧数*1000，每次长距离滑动变化一帧。
                _guiReadPositionBar.Maximum = (int)numOfFrames - 1;
                _guiReadPositionBar.LargeChange = 1;
                _guiReadPositionBar.SmallChange = 1;
                _guiReadPosValue.Text = "1";

                #endregion

                if (_guiReadPositionBar.Value != 0) { _guiReadPositionBar.Value = 0; }
                else { ReadDataAndDisplay(); }
                
                // 读取数据成功，允许用户的后续操作。
                _guiReadControlSetting.Enabled = true;
            }
            catch (Exception exception)
            {
                // 出现异常，提示用户，关闭文件并禁止后续操作。
                MessageBox.Show(exception.Message + Environment.NewLine + exception.StackTrace);
                _iqFrameFile?.Close();
                _iqFrameFile = null;
                _guiReadControlSetting.Enabled = false;
            }
        }

        private void GuiReadPositionBar_ValueChanged(object sender, EventArgs e)
        {
            // 如果当前已启动连续读取，则TrackerBar的控件值改变是因BackgroundWorker更新显示而触发，无需任何处理，直接返回。
            if(_isContinuousReading) { return; }

            // 根据TrackBar控件的当前值（相对于数据起始的毫秒数），计算读取位置并显示。
            long readPosition = _guiReadPositionBar.Value;
            _guiReadPosValue.Text = Convert.ToString(readPosition + 1);

            // 设置读取位置，读取数据并显示。
            _iqFrameFile.Seek(readPosition, SeekOrigin.Begin);
            ReadDataAndDisplay();
        }

        private void GuiStart_Click(object sender, EventArgs e)
        {
            // 启动BackgroundWorker。
            _bgWorker.RunWorkerAsync();

            // 设置标志位。
            _isContinuousReading = true;

            // 设置控件状态。简化实现：开启连续读取之后，不允许拖动TrackerBar。
            _guiReadPositionBar.Enabled = false;
            _guiFileSelectionParam.Enabled = false;
            _guiStart.Enabled = false;
            _guiStop.Enabled = true;

        }

        private void GuiStop_Click(object sender, EventArgs e)
        {
            // 停止BackgroundWorker。
            _bgWorker.CancelAsync();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_bgWorker.IsBusy)
            {
                // 简化实现：如果当前正在连续读取，不允许关闭窗口。
                MessageBox.Show("当前正在连续读取，请先停止读取任务。");
                e.Cancel = true;
            }
            else
            {
                // 可以正常关闭窗口，先关闭已经打开的文件。
                _iqFrameFile?.Close();
            }
        }

        #endregion

        #region------------------------- BackgroundWorker实现文件连续读取 -------------------------

        private void BgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            // 获取BackgroudWorker对象，用于运行控制。
            var bgWorker = sender as BackgroundWorker;

            while(true)
            {
                // 从文件当前位置开始，持续读取数据并显示，直至文件末尾。简化实现：直接在此更新显示。
                this.Invoke(new Action(() => { ReadDataAndDisplay(); }));

                // 向GUI更新进度，不使用ProgressPercentage，而是发送当前读取位置（相对于文件起始位置的时间，ms）。
                bgWorker.ReportProgress(0, (int)(_iqFrameFile.Position - 1));

                // 若已到达文件末尾，或用户取消了操作，则退出循环。
                if (_iqFrameFile.Position + 1 > _iqFrameFile.NumberOfFrames) { break; }
                if (bgWorker.CancellationPending) { e.Cancel = true; break; }
                Thread.Sleep(10);
            }
        }

        private void BgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            _guiReadPositionBar.Value = (int)e.UserState;
            _guiReadPosValue.Text = Convert.ToString(_guiReadPositionBar.Value + 1);
        }

        private void BgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            #region------------------------- 显示BackgroundWorker的运行结果。 -------------------------

            if (e.Error != null)
            {
                // BackgroundWorker的运行中出现异常，则提示用户。
                MessageBox.Show(e.Error.Message + Environment.NewLine + e.Error.StackTrace);
            }
            else
            {
                if (!e.Cancelled)
                {
                    // BackgroundWorker未发生异常，用户也未取消，说明任务已正常结束。
                    MessageBox.Show("已到达文件末尾。");
                }
                else
                {
                    // 用户主动取消了操作，则不作任何提示。
                }
            }

            #endregion

            // 任务已经结束，设置GUI控件状态。
            _guiFileSelectionParam.Enabled = true;
            _guiReadPositionBar.Enabled = true;
            _guiStart.Enabled = true;
            _guiStop.Enabled = false;

            // 设置标志位。
            _isContinuousReading = false;
        }

        #endregion

        #region------------------------- 私有方法 -------------------------

        /// <summary>
        /// 在文件的当前位置读取IQ数据，计算频谱并显示。
        /// </summary>
        private void ReadDataAndDisplay()
        {

            // 在当前位置读取IQ数据，若“当前位置 + 读取长度”已超出文件长度，则先将“当前读取位置”向前移动。
            if (_iqFrameFile.Position + 1 > _iqFrameFile.NumberOfFrames) { _iqFrameFile.Seek(-1, SeekOrigin.End); }

            // 读取数据。
            _iqFrameFile.Read(_shortIQAllChannels);

            // 获取数据采样信息。
            int numOfChannels = _iqFrameFile.Storage.NumberOfChannels;
            double sampleRate = _iqFrameFile.Sampling.SampleRate;
            // 获取信号带宽，用于计算频谱。若文件头中未保存带宽信息，则使用0.8倍采样率作为分析带宽。
            double bandwidth = _iqFrameFile.Sampling.Channels[0].Bandwidth;

            //获取信号的中心频率。
            double frequency = _iqFrameFile.Sampling.Channels[0].RFFrequency;
            if (bandwidth <= 0) { bandwidth = sampleRate * 0.8; }

            // 将读取的数据按照各通道依次计算频谱。
            for (int channelIndex = 0; channelIndex < numOfChannels; channelIndex++)
            {
                // 获取当前通道数据的电压换算因子。
                double scaleFactor = _iqFrameFile.Sampling.Channels[channelIndex].GetScaleFactor();

                // 将当前通道的I16数据转换为Complex。
                int indexInRawArray;
                for(int i = 0; i <  _iqFrameFile.Frame.Length; i++)
                {
                    // 计算当前sample在通道交织（Channel-Interleaved）数组中的位置。
                    indexInRawArray = (i * numOfChannels + channelIndex) * 2;
                    _complexIQOneChannel[i] = new Complex(_shortIQAllChannels[indexInRawArray] * scaleFactor, _shortIQAllChannels[indexInRawArray + 1] * scaleFactor);
                }

                // 计算频谱。简化实现：调用EasyPoweSpectrum。
                EasySpectrum.AutoPowerSpectrum(_complexIQOneChannel, sampleRate, bandwidth, _spectrumOneChannel);

                // 将当前通道的频谱填入多通道频谱的2维数组。
                ArrayManipulation.ReplaceArraySubset(_spectrumOneChannel, ref _spectrumAllChannels, channelIndex);
            }

            // 计算f0/df，并显示频谱。
            _guiSpectrumChart.Plot(_spectrumAllChannels, -bandwidth / 2 + frequency, bandwidth / (_spectrumOneChannel.Length));

        }

        #endregion
    }
}
