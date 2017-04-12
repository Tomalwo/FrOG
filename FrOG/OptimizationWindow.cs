﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace FrOG
{
    public partial class OptimizationWindow : Form
    {
        private readonly OptimizationComponent _frogComponent;

        public OptimizationWindow(OptimizationComponent component)
        {
            //Refers to the FrOG Window: Do not rename!!!
            InitializeComponent();

            //Referes to the FrOG component on the Grasshopper canvas
            _frogComponent = component;

            //Fill and set Combobox
            foreach (var preset in SolverList.PresetNames) comboBoxPresets.Items.Add(preset);
            comboBoxPresets.SelectedIndex = 0;

            //Log Default File Name
            textBoxLogName.Text = String.Format("{0}_log", DateTime.Now.ToString("yyMMdd"));

            //Hide Save State Tab
            //Tabs.TabPages.Remove(Tabs.TabPages[3]);

            //Disable Chart Axis
            bestValueChart.ChartAreas[0].AxisX.Enabled = AxisEnabled.False;
            bestValueChart.ChartAreas[0].AxisX2.Enabled = AxisEnabled.False;
            bestValueChart.ChartAreas[0].AxisY.Enabled = AxisEnabled.False;
            bestValueChart.ChartAreas[0].AxisY2.Enabled = AxisEnabled.False;

            //Setting Border for the Chart Area
            bestValueChart.ChartAreas[0].BorderWidth = 0;

            //Lock Stop Button
            buttonStop.Enabled = false;

            //Hide Labels
            labelIteration.Visible = false;
            labelBestValue.Visible = false;

            //Initilize Backgroundworker
            //http://www.codeproject.com/Articles/634146/Background-Thread-Let-me-count-the-ways
            backgroundWorkerSolver.DoWork += OptimizationLoop.RunOptimizationLoopMultiple;
            backgroundWorkerSolver.ProgressChanged += UpdateChart;
            backgroundWorkerSolver.RunWorkerCompleted += ReleaseButtons;
            backgroundWorkerSolver.WorkerReportsProgress = true;
            backgroundWorkerSolver.WorkerSupportsCancellation = true;
        }

        //Buttons
        private void cancelButton_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            backgroundWorkerSolver.CancelAsync();
            buttonStop.Enabled = false;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            //Lock Buttons
            ButtonStart.Enabled = false;
            buttonOK.Enabled = false;
            buttonCancel.Enabled = false;

            //Options
            OptimizationLoop.BolMaxIter = CheckBoxIterations.Checked;
            if (CheckBoxIterations.Checked) OptimizationLoop.MaxIter = (int)numUpDownIterations.Value;

            OptimizationLoop.BolMaxIterNoProgress = CheckBoxImprovement.Checked;
            if (CheckBoxImprovement.Checked) OptimizationLoop.MaxIterNoProgress = (int)numUpDownImprovement.Value;

            OptimizationLoop.BolMaxDuration = CheckBoxDuration.Checked;
            if (CheckBoxDuration.Checked) OptimizationLoop.MaxDuration = (int)numUpDownDuration.Value;

            OptimizationLoop.BolLog = CheckBoxLog.Checked;
            if (CheckBoxLog.Checked) OptimizationLoop.LogName = textBoxLogName.Text;

            //OptimizationLoop.SaveStateName = textBoxStateName.Text;
            //OptimizationLoop.SaveStateFrequency = (int)numUpDownSaveStateFrequency.Value;
            OptimizationLoop.BolMaximize = radioButtonMaximize.Checked;
            OptimizationLoop.ExpertSettings = textBoxExpertSettings.Text.Replace(Environment.NewLine, " ");
            OptimizationLoop.PresetIndex = comboBoxPresets.SelectedIndex;

            //Number of Runs
            OptimizationLoop.BolRuns = CheckBoxRuns.Checked;
            if (CheckBoxRuns.Checked) OptimizationLoop.Runs = (int)numUpDownRuns.Value;
            else OptimizationLoop.Runs = 1;

            //Start Optimization
            backgroundWorkerSolver.RunWorkerAsync(_frogComponent);

            //Unlock Stop
            buttonStop.Enabled = true;
        }

        private void ReleaseButtons(object sender, RunWorkerCompletedEventArgs e)
        {
            ButtonStart.Enabled = true;
            buttonOK.Enabled = true;
            buttonCancel.Enabled = true;
            buttonStop.Enabled = false;
        }

        //Chart
        private void UpdateChart(object sender, ProgressChangedEventArgs e)
        {
            //Clear chart data
            bestValueChart.Series.Clear();

            //Prepare new series
            var series1 = new Series
            {
                Name = "BestValueSeries",
                Color = Color.Pink,
                IsVisibleInLegend = false,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Area,
            };

            bestValueChart.Series.Add(series1);

            //Update chart data
            var values = (List<double>)e.UserState;
            var iteration = values.Count;

            for (var i = 0; i < iteration; i++)
            {
                series1.Points.AddXY(i, values[i]);
            }

            //Update labels
            labelIteration.Text = "Iteration: " + iteration;
            labelBestValue.Text = "Best Value: " + Math.Round(values[iteration - 1], 3);

            //Show Labels
            labelIteration.Visible = true;
            labelBestValue.Visible = true;

            //Redraw chart
            bestValueChart.Invalidate();
        }

        //Check Boxes
        private void CheckBoxIterations_CheckedChanged(object sender, EventArgs e)
        {
            numUpDownIterations.Enabled = CheckBoxIterations.Checked;
        }

        private void CheckBoxImprovement_CheckedChanged(object sender, EventArgs e)
        {
            numUpDownImprovement.Enabled = CheckBoxImprovement.Checked;
        }

        private void CheckBoxDuration_CheckedChanged(object sender, EventArgs e)
        {
            numUpDownDuration.Enabled = CheckBoxDuration.Checked;
        }

        private void CheckBoxLog_CheckedChanged(object sender, EventArgs e)
        {
            textBoxLogName.Enabled = CheckBoxLog.Checked;
        }

        private void CheckBoxRuns_CheckedChanged(object sender, EventArgs e)
        {
            numUpDownRuns.Enabled = CheckBoxRuns.Checked;
        }

        private void comboBoxPresets_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void FrOGWindow_Load(object sender, EventArgs e)
        {

        }
    }
}
