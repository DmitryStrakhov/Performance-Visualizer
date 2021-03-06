﻿#if DEBUG

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Algorithms_Performance_Visualizer.Base;
using Algorithms_Performance_Visualizer.Views;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Algorithms_Performance_Visualizer.Tests {
    #region TestSearchingView

    [ToolboxItem(false)]
    class TestSearchingView : SearchingView {
        readonly Label progressLabelCore;
        readonly Button btnStartCore;

        public TestSearchingView() {
            this.progressLabelCore = this.GetControl<Label>("progressLabel");
            this.btnStartCore = this.GetControl<Button>("btnStart");
        }
        public Label ProgressLabel { get { return progressLabelCore; } }
        public Button StartButton { get { return btnStartCore; } }
    }

    #endregion

    #region TestSearchingViewForm

    class TestSearchingViewForm : BaseTestForm<TestSearchingView> {
        public TestSearchingViewForm() {
        }
        protected override void InitializeForm() {
            base.InitializeForm();
            Size = new Size(1226, 646);
        }
        protected override void InitializeTestControl(TestSearchingView control) {
            control.Dock = DockStyle.Fill;
        }
    }

    #endregion

    [TestClass]
    public class SearchingViewTests {
        TestSearchingViewForm form;
        TestSearchingView view;
        SearchingViewController controller;

        [TestInitialize]
        public void SetUp() {
            this.form = new TestSearchingViewForm();
            this.view = this.form.Control;
            this.controller = View.Controller;
            this.form.Show();
            this.form.Visible = true;
            this.form.Update();
        }
        [TestCleanup]
        public void TearDown() {
            this.form.Dispose();
            this.form = null;
            this.view = null;
            this.controller = null;
        }
        TestSearchingView View { get { return view; } }
        SearchingViewController Controller { get { return controller; } }

        [TestMethod, Ignore]
        public void ShowUpTest() {
            while(this.form.Visible) {
                Application.DoEvents();
            }
        }
        [TestMethod]
        public void ControllerDefaultsTest() {
            Assert.AreEqual(string.Empty, Controller.Progress);
            Assert.AreEqual(ControllerState.Wait, Controller.State);
        }
        [TestMethod]
        public void ControllerToViewBindingsTest() {
            Assert.AreEqual(string.Empty, View.ProgressLabel.Text);
            Controller.Progress = "stage #1";
            Assert.AreEqual("stage #1", View.ProgressLabel.Text);
            Controller.Progress = "stage #2";
            Assert.AreEqual("stage #2", View.ProgressLabel.Text);
            Controller.Progress = string.Empty;
            Assert.AreEqual(string.Empty, View.ProgressLabel.Text);
        }
        [TestMethod]
        public void ViewStartStopButtonTextTest() {
            Controller.State = ControllerState.Wait;
            Assert.AreEqual("Start", View.StartButton.Text);
            Controller.State = ControllerState.Active;
            Assert.AreEqual("Stop", View.StartButton.Text);
            Controller.State = ControllerState.Wait;
            Assert.AreEqual("Start", View.StartButton.Text);
        }
    }

}
#endif