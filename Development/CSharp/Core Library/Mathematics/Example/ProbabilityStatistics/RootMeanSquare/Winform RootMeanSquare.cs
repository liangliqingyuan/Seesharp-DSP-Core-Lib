﻿using System;
using System.Windows.Forms;
using SeeSharpTools.JXI.Mathematics.ProbabilityStatistics;
using System.Diagnostics;

namespace SeesharpTools.JXI.Mathematics.example
{
    /// <summary>
    /// 版权所有(C)2017，上海聚星仪器有限公司
    /// Copyright (C) 2017, Shanghai Juxing Instrument Co., Ltd.
    /// 内容摘要: 本Demo为统计分析类库的Demo
    ///           产生一组随机数并计算其均方根
    /// Abstract: This demo is a demo of the statistical analysis class library.
    ///           Generate a set of random numbers and calculate their root mean square
    /// Completion date: September 17, 2017
    /// Version: 1.0
    /// Author: Liu Yuhui， JXI
    public partial class RootMeanSquareForm : Form
    {
        #region private Fields              
        /// <summary>
        /// 待计算的数据
        /// Data to be calculated
        /// </summary>
        private double[] data;

        #endregion

        #region Construct
        public RootMeanSquareForm()
        {
            InitializeComponent();
            RMSEvent(null, null);
            EfficiencyEvent(null, null);
        }
        #endregion

        #region Event Handler
       
        /// <summary>
        /// 计算RMS
        /// calculaate RMS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RMSEvent(object sender, EventArgs e)
        {
            data = new double[(int)nudLength.Value];
            RandomSequence(ref data, 0, 1);
            easyChart1.Plot(data);           
            txtboxRMS.Text = ProbabilityStatistics.RootMeanSquare(data).ToString();
            EfficiencyEvent(null, null);
        }

        /// <summary>
        /// 运行该方法指定次数计算消耗时间
        /// Loop to method to benchmark
        /// </summary>
        private void EfficiencyEvent(object sender, EventArgs e)
        {
            int loopTimes = (int)nudTestTimes.Value;                        
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < loopTimes; i++)
            {
                ProbabilityStatistics.RootMeanSquare(data);
            }
            stopwatch.Stop();
            txtboxTimeElapsed.Text = stopwatch.ElapsedMilliseconds.ToString() + "ms";
        }
        #endregion

        #region Methods
        /// <summary>
        /// double RandomSequence Generation
        /// </summary>
        /// <param name="length"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        public void RandomSequence(ref double[] sequence, double min, double max)
        {
            if (sequence.Length < 1)
            {
                throw new ArgumentException();
            }
            int length = sequence.Length;
            Random r = new Random();
            for (int i = 0; i < length; i++)
            {
                sequence[i] = r.NextDouble() * (max - min) + min;
            }
        }
        #endregion

        
    }
}
