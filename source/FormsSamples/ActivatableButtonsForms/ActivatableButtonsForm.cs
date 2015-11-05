//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

namespace ActivatableButtonsForms
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using EyeXFramework;

    public partial class ActivatableButtonsForm : Form
    {
        const float HueStep = 0.075f;
        const float BrightnessStep = 0.1f;

        public ActivatableButtonsForm()
        {
            InitializeComponent();
            UpdateButtonColors();

            // Make the buttons on the form direct clickable using the Activatable behavior.
            Program.EyeXHost.Connect(behaviorMap1);
            behaviorMap1.Add(buttonHueUp, new ActivatableBehavior(OnButtonActivated));
            behaviorMap1.Add(buttonHueDown, new ActivatableBehavior(OnButtonActivated));
            behaviorMap1.Add(buttonBrightnessUp, new ActivatableBehavior(OnButtonActivated));
            behaviorMap1.Add(buttonBrightnessDown, new ActivatableBehavior(OnButtonActivated));
        }

        /// <summary>
        /// Event handler invoked when a button is gaze clicked.
        /// </summary>
        /// <param name="sender">The control that received the gaze click.</param>
        private void OnButtonActivated(object sender, EventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                // make the button Do Its Thing.
                button.PerformClick();
            }
        }

        private void UpdateButtonColors()
        {
            buttonHueUp.BackColor = ModifyHue(panelColorSample.BackColor, +HueStep);
            buttonHueDown.BackColor = ModifyHue(panelColorSample.BackColor, -HueStep);
            buttonBrightnessUp.BackColor = ModifyBrightness(panelColorSample.BackColor, +BrightnessStep);
            buttonBrightnessDown.BackColor = ModifyBrightness(panelColorSample.BackColor, -BrightnessStep);
        }

        private Color ModifyHue(Color color, float change)
        {
            var hsb = new HsbColor(color);
            hsb.Hue += change;
            return hsb.ToRgb();
        }

        private Color ModifyBrightness(Color color, float change)
        {
            var hsb = new HsbColor(color);
            hsb.Brightness += change;
            return hsb.ToRgb();
        }

        private void buttonHueDown_Click(object sender, EventArgs e)
        {
            panelColorSample.BackColor = ModifyHue(panelColorSample.BackColor, -HueStep);
            UpdateButtonColors();
        }

        private void buttonHueUp_Click(object sender, EventArgs e)
        {
            panelColorSample.BackColor = ModifyHue(panelColorSample.BackColor, +HueStep);
            UpdateButtonColors();
        }

        private void buttonBrightnessUp_Click(object sender, EventArgs e)
        {
            panelColorSample.BackColor = ModifyBrightness(panelColorSample.BackColor, +BrightnessStep);
            UpdateButtonColors();
        }

        private void buttonBrightnessDown_Click(object sender, EventArgs e)
        {
            panelColorSample.BackColor = ModifyBrightness(panelColorSample.BackColor, -BrightnessStep);
            UpdateButtonColors();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // Make the space key trigger activation ("direct click") events.
            // The direct click key configured in EyeX Interaction settings will still work.
            // NOTE: Use this feature with caution, since it is much more powerful to use the same 
            // key for clicking throughout the system.
            if (keyData == Keys.Space)
            {
                Program.EyeXHost.TriggerActivation();
                return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
