using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace FrOG
{
    partial class OptimizationWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptimizationWindow));
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.buttonCancel = new System.Windows.Forms.Button();
            this.buttonOK = new System.Windows.Forms.Button();
            this.tabOptimize = new System.Windows.Forms.TabPage();
            this.labelBestValue = new System.Windows.Forms.Label();
            this.labelIteration = new System.Windows.Forms.Label();
            this.bestValueChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupOptimizationStartStop = new System.Windows.Forms.GroupBox();
            this.buttonStop = new System.Windows.Forms.Button();
            this.ButtonStart = new System.Windows.Forms.Button();
            this.GroupOptimizationAlgorithm = new System.Windows.Forms.GroupBox();
            this.comboBoxPresets = new System.Windows.Forms.ComboBox();
            this.GroupOptimizationType = new System.Windows.Forms.GroupBox();
            this.radioButtonMinimize = new System.Windows.Forms.RadioButton();
            this.radioButtonMaximize = new System.Windows.Forms.RadioButton();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.tabSettings = new System.Windows.Forms.TabPage();
            this.Benchmarking = new System.Windows.Forms.GroupBox();
            this.textBoxLogName = new System.Windows.Forms.TextBox();
            this.numUpDownRuns = new System.Windows.Forms.NumericUpDown();
            this.CheckBoxRuns = new System.Windows.Forms.CheckBox();
            this.CheckBoxLog = new System.Windows.Forms.CheckBox();
            this.Settings_Stopping_Conditions = new System.Windows.Forms.GroupBox();
            this.numUpDownDuration = new System.Windows.Forms.NumericUpDown();
            this.numUpDownImprovement = new System.Windows.Forms.NumericUpDown();
            this.numUpDownIterations = new System.Windows.Forms.NumericUpDown();
            this.CheckBoxDuration = new System.Windows.Forms.CheckBox();
            this.CheckBoxImprovement = new System.Windows.Forms.CheckBox();
            this.CheckBoxIterations = new System.Windows.Forms.CheckBox();
            this.tabExpert = new System.Windows.Forms.TabPage();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.textBoxExpertSettings = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorkerSolver = new System.ComponentModel.BackgroundWorker();
            this.tabOptimize.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bestValueChart)).BeginInit();
            this.groupOptimizationStartStop.SuspendLayout();
            this.GroupOptimizationAlgorithm.SuspendLayout();
            this.GroupOptimizationType.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.tabSettings.SuspendLayout();
            this.Benchmarking.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRuns)).BeginInit();
            this.Settings_Stopping_Conditions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDuration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownImprovement)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownIterations)).BeginInit();
            this.tabExpert.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonCancel
            // 
            resources.ApplyResources(this.buttonCancel, "buttonCancel");
            this.buttonCancel.Name = "buttonCancel";
            this.buttonCancel.UseVisualStyleBackColor = true;
            this.buttonCancel.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // buttonOK
            // 
            resources.ApplyResources(this.buttonOK, "buttonOK");
            this.buttonOK.Name = "buttonOK";
            this.buttonOK.UseVisualStyleBackColor = true;
            this.buttonOK.Click += new System.EventHandler(this.okButton_Click);
            // 
            // tabOptimize
            // 
            this.tabOptimize.Controls.Add(this.labelBestValue);
            this.tabOptimize.Controls.Add(this.labelIteration);
            this.tabOptimize.Controls.Add(this.bestValueChart);
            this.tabOptimize.Controls.Add(this.groupOptimizationStartStop);
            this.tabOptimize.Controls.Add(this.GroupOptimizationAlgorithm);
            this.tabOptimize.Controls.Add(this.GroupOptimizationType);
            resources.ApplyResources(this.tabOptimize, "tabOptimize");
            this.tabOptimize.Name = "tabOptimize";
            this.tabOptimize.UseVisualStyleBackColor = true;
            // 
            // labelBestValue
            // 
            resources.ApplyResources(this.labelBestValue, "labelBestValue");
            this.labelBestValue.BackColor = System.Drawing.Color.Transparent;
            this.labelBestValue.Name = "labelBestValue";
            // 
            // labelIteration
            // 
            resources.ApplyResources(this.labelIteration, "labelIteration");
            this.labelIteration.BackColor = System.Drawing.Color.Transparent;
            this.labelIteration.Name = "labelIteration";
            // 
            // bestValueChart
            // 
            this.bestValueChart.BackColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LineWidth = 0;
            chartArea1.AxisX2.LineWidth = 0;
            chartArea1.AxisY.LineWidth = 0;
            chartArea1.AxisY2.LineWidth = 0;
            chartArea1.BorderWidth = 0;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 100F;
            chartArea1.InnerPlotPosition.Width = 100F;
            chartArea1.IsSameFontSizeForAllAxes = true;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.bestValueChart.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.bestValueChart.Legends.Add(legend1);
            resources.ApplyResources(this.bestValueChart, "bestValueChart");
            this.bestValueChart.Name = "bestValueChart";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Area;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.bestValueChart.Series.Add(series1);
            // 
            // groupOptimizationStartStop
            // 
            this.groupOptimizationStartStop.Controls.Add(this.buttonStop);
            this.groupOptimizationStartStop.Controls.Add(this.ButtonStart);
            resources.ApplyResources(this.groupOptimizationStartStop, "groupOptimizationStartStop");
            this.groupOptimizationStartStop.Name = "groupOptimizationStartStop";
            this.groupOptimizationStartStop.TabStop = false;
            // 
            // buttonStop
            // 
            resources.ApplyResources(this.buttonStop, "buttonStop");
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // ButtonStart
            // 
            resources.ApplyResources(this.ButtonStart, "ButtonStart");
            this.ButtonStart.Name = "ButtonStart";
            this.ButtonStart.UseVisualStyleBackColor = true;
            this.ButtonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // GroupOptimizationAlgorithm
            // 
            this.GroupOptimizationAlgorithm.Controls.Add(this.comboBoxPresets);
            resources.ApplyResources(this.GroupOptimizationAlgorithm, "GroupOptimizationAlgorithm");
            this.GroupOptimizationAlgorithm.Name = "GroupOptimizationAlgorithm";
            this.GroupOptimizationAlgorithm.TabStop = false;
            // 
            // comboBoxPresets
            // 
            this.comboBoxPresets.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxPresets.FormattingEnabled = true;
            resources.ApplyResources(this.comboBoxPresets, "comboBoxPresets");
            this.comboBoxPresets.Name = "comboBoxPresets";
            this.comboBoxPresets.SelectedIndexChanged += new System.EventHandler(this.comboBoxPresets_SelectedIndexChanged);
            // 
            // GroupOptimizationType
            // 
            this.GroupOptimizationType.Controls.Add(this.radioButtonMinimize);
            this.GroupOptimizationType.Controls.Add(this.radioButtonMaximize);
            resources.ApplyResources(this.GroupOptimizationType, "GroupOptimizationType");
            this.GroupOptimizationType.Name = "GroupOptimizationType";
            this.GroupOptimizationType.TabStop = false;
            // 
            // radioButtonMinimize
            // 
            resources.ApplyResources(this.radioButtonMinimize, "radioButtonMinimize");
            this.radioButtonMinimize.Checked = true;
            this.radioButtonMinimize.Name = "radioButtonMinimize";
            this.radioButtonMinimize.TabStop = true;
            this.radioButtonMinimize.UseVisualStyleBackColor = true;
            // 
            // radioButtonMaximize
            // 
            resources.ApplyResources(this.radioButtonMaximize, "radioButtonMaximize");
            this.radioButtonMaximize.Name = "radioButtonMaximize";
            this.radioButtonMaximize.UseVisualStyleBackColor = true;
            // 
            // Tabs
            // 
            resources.ApplyResources(this.Tabs, "Tabs");
            this.Tabs.Controls.Add(this.tabOptimize);
            this.Tabs.Controls.Add(this.tabSettings);
            this.Tabs.Controls.Add(this.tabExpert);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            // 
            // tabSettings
            // 
            this.tabSettings.Controls.Add(this.Benchmarking);
            this.tabSettings.Controls.Add(this.Settings_Stopping_Conditions);
            resources.ApplyResources(this.tabSettings, "tabSettings");
            this.tabSettings.Name = "tabSettings";
            this.tabSettings.UseVisualStyleBackColor = true;
            // 
            // Benchmarking
            // 
            this.Benchmarking.Controls.Add(this.textBoxLogName);
            this.Benchmarking.Controls.Add(this.numUpDownRuns);
            this.Benchmarking.Controls.Add(this.CheckBoxRuns);
            this.Benchmarking.Controls.Add(this.CheckBoxLog);
            resources.ApplyResources(this.Benchmarking, "Benchmarking");
            this.Benchmarking.Name = "Benchmarking";
            this.Benchmarking.TabStop = false;
            // 
            // textBoxLogName
            // 
            resources.ApplyResources(this.textBoxLogName, "textBoxLogName");
            this.textBoxLogName.Name = "textBoxLogName";
            // 
            // numUpDownRuns
            // 
            resources.ApplyResources(this.numUpDownRuns, "numUpDownRuns");
            this.numUpDownRuns.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownRuns.Name = "numUpDownRuns";
            this.numUpDownRuns.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // CheckBoxRuns
            // 
            resources.ApplyResources(this.CheckBoxRuns, "CheckBoxRuns");
            this.CheckBoxRuns.Name = "CheckBoxRuns";
            this.CheckBoxRuns.UseVisualStyleBackColor = true;
            this.CheckBoxRuns.CheckedChanged += new System.EventHandler(this.CheckBoxRuns_CheckedChanged);
            // 
            // CheckBoxLog
            // 
            resources.ApplyResources(this.CheckBoxLog, "CheckBoxLog");
            this.CheckBoxLog.Name = "CheckBoxLog";
            this.CheckBoxLog.UseVisualStyleBackColor = true;
            this.CheckBoxLog.CheckedChanged += new System.EventHandler(this.CheckBoxLog_CheckedChanged);
            // 
            // Settings_Stopping_Conditions
            // 
            this.Settings_Stopping_Conditions.Controls.Add(this.numUpDownDuration);
            this.Settings_Stopping_Conditions.Controls.Add(this.numUpDownImprovement);
            this.Settings_Stopping_Conditions.Controls.Add(this.numUpDownIterations);
            this.Settings_Stopping_Conditions.Controls.Add(this.CheckBoxDuration);
            this.Settings_Stopping_Conditions.Controls.Add(this.CheckBoxImprovement);
            this.Settings_Stopping_Conditions.Controls.Add(this.CheckBoxIterations);
            resources.ApplyResources(this.Settings_Stopping_Conditions, "Settings_Stopping_Conditions");
            this.Settings_Stopping_Conditions.Name = "Settings_Stopping_Conditions";
            this.Settings_Stopping_Conditions.TabStop = false;
            // 
            // numUpDownDuration
            // 
            resources.ApplyResources(this.numUpDownDuration, "numUpDownDuration");
            this.numUpDownDuration.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.numUpDownDuration.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownDuration.Name = "numUpDownDuration";
            this.numUpDownDuration.Value = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            // 
            // numUpDownImprovement
            // 
            resources.ApplyResources(this.numUpDownImprovement, "numUpDownImprovement");
            this.numUpDownImprovement.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownImprovement.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownImprovement.Name = "numUpDownImprovement";
            this.numUpDownImprovement.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            // 
            // numUpDownIterations
            // 
            resources.ApplyResources(this.numUpDownIterations, "numUpDownIterations");
            this.numUpDownIterations.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.numUpDownIterations.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numUpDownIterations.Name = "numUpDownIterations";
            this.numUpDownIterations.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numUpDownIterations.ValueChanged += new System.EventHandler(this.numUpDownIterations_ValueChanged);
            // 
            // CheckBoxDuration
            // 
            resources.ApplyResources(this.CheckBoxDuration, "CheckBoxDuration");
            this.CheckBoxDuration.Name = "CheckBoxDuration";
            this.CheckBoxDuration.UseVisualStyleBackColor = true;
            this.CheckBoxDuration.CheckedChanged += new System.EventHandler(this.CheckBoxDuration_CheckedChanged);
            // 
            // CheckBoxImprovement
            // 
            resources.ApplyResources(this.CheckBoxImprovement, "CheckBoxImprovement");
            this.CheckBoxImprovement.Name = "CheckBoxImprovement";
            this.CheckBoxImprovement.UseVisualStyleBackColor = true;
            this.CheckBoxImprovement.CheckedChanged += new System.EventHandler(this.CheckBoxImprovement_CheckedChanged);
            // 
            // CheckBoxIterations
            // 
            resources.ApplyResources(this.CheckBoxIterations, "CheckBoxIterations");
            this.CheckBoxIterations.Name = "CheckBoxIterations";
            this.CheckBoxIterations.UseVisualStyleBackColor = true;
            this.CheckBoxIterations.CheckedChanged += new System.EventHandler(this.CheckBoxIterations_CheckedChanged);
            // 
            // tabExpert
            // 
            this.tabExpert.Controls.Add(this.groupBox2);
            resources.ApplyResources(this.tabExpert, "tabExpert");
            this.tabExpert.Name = "tabExpert";
            this.tabExpert.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.textBoxExpertSettings);
            this.groupBox2.Controls.Add(this.label3);
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // textBoxExpertSettings
            // 
            resources.ApplyResources(this.textBoxExpertSettings, "textBoxExpertSettings");
            this.textBoxExpertSettings.Name = "textBoxExpertSettings";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // OptimizationWindow
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ControlBox = false;
            this.Controls.Add(this.buttonCancel);
            this.Controls.Add(this.buttonOK);
            this.Controls.Add(this.Tabs);
            this.Name = "OptimizationWindow";
            this.Load += new System.EventHandler(this.FrOGWindow_Load);
            this.tabOptimize.ResumeLayout(false);
            this.tabOptimize.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bestValueChart)).EndInit();
            this.groupOptimizationStartStop.ResumeLayout(false);
            this.GroupOptimizationAlgorithm.ResumeLayout(false);
            this.GroupOptimizationType.ResumeLayout(false);
            this.GroupOptimizationType.PerformLayout();
            this.Tabs.ResumeLayout(false);
            this.tabSettings.ResumeLayout(false);
            this.Benchmarking.ResumeLayout(false);
            this.Benchmarking.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownRuns)).EndInit();
            this.Settings_Stopping_Conditions.ResumeLayout(false);
            this.Settings_Stopping_Conditions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownDuration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownImprovement)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numUpDownIterations)).EndInit();
            this.tabExpert.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private Button buttonCancel;
        private Button buttonOK;
        private GroupBox Settings_Stopping_Conditions;
        private CheckBox CheckBoxImprovement;
        private CheckBox CheckBoxIterations;
        private TabPage tabSettings;
        private TabControl Tabs;
        private RadioButton radioButtonMaximize;
        private RadioButton radioButtonMinimize;
        private GroupBox GroupOptimizationType;
        private GroupBox GroupOptimizationAlgorithm;
        private Button ButtonStart;
        private Button buttonStop;
        private GroupBox groupOptimizationStartStop;
        private TabPage tabOptimize;
        private CheckBox CheckBoxDuration;
        private NumericUpDown numUpDownImprovement;
        private NumericUpDown numUpDownIterations;
        private NumericUpDown numUpDownDuration;
        private GroupBox Benchmarking;
        private TextBox textBoxLogName;
        private NumericUpDown numUpDownRuns;
        private CheckBox CheckBoxRuns;
        private CheckBox CheckBoxLog;
        private TabPage tabExpert;
        private GroupBox groupBox2;
        private TextBox textBoxExpertSettings;
        private Label label3;
        private ComboBox comboBoxPresets;
        private Chart bestValueChart;
        private System.ComponentModel.BackgroundWorker backgroundWorkerSolver;
        private Label labelBestValue;
        private Label labelIteration;
    }
}