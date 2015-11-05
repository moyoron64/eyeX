//-----------------------------------------------------------------------
// Copyright 2014 Tobii Technology AB. All rights reserved.
//-----------------------------------------------------------------------

namespace ActivatableNotesForms
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using EyeXFramework;

    public partial class ActivatableNotesForm : Form
    {
        private readonly List<string> quotes = new List<string>()
            {
                "You miss 100 percent of the shots you never take.\n—Wayne Gretzky",
                "I always wanted to be somebody, but now I realize I should have been more specific.\n—Lily Tomlin",
                "To the man who only has a hammer, everything he encounters begins to look like a nail.\n—Abraham Maslow",
                "I am extraordinarily patient, provided I get my own way in the end.\n—Margaret Thatcher",
                "Those who believe in telekinetics, raise my hand.\n—Kurt Vonnegut",
                "It is more important to know where you are going than to get there quickly.\n—Mabel Newcomber",
                "If you eat a frog first thing in the morning, the rest of your day will be wonderful.\n—Mark Twain",
                "Age is not important unless you're a cheese.\n—Helen Hayes",
                "If you have to eat a frog, don’t look at it for too long.\n—Mark Twain",
                "A thing is mighty big when time and distance cannot shrink it.\n—Zora Neale Hurston",
            };

        private readonly List<Color> colorsApprovedByTheNotesAuthority = new List<Color>()
        {
            Color.LightYellow, Color.Lime, Color.IndianRed, Color.MediumSlateBlue, Color.MediumOrchid, Color.Linen, Color.MintCream
        };

        private Random _random = new Random();
       
        public ActivatableNotesForm()
        {
            InitializeComponent();

            // Make the buttons on the form direct clickable using the Activatable behavior.
            Program.EyeXHost.Connect(behaviorMap1);
            behaviorMap1.Add(button1, new ActivatableBehavior(OnButtonActivated));
            behaviorMap1.Add(button2, new ActivatableBehavior(OnButtonActivated));

            AddNotes();
        }

        private void OnButtonActivated(object sender, EventArgs e)
        {
            ((Button)sender).PerformClick();
        }

        private void AddNotes()
        {
            var size = ClientSize;

            foreach (var quote in quotes)
            {
                var note = new NoteControl();
                note.Text = quote;
                note.BackColor = colorsApprovedByTheNotesAuthority[_random.Next(colorsApprovedByTheNotesAuthority.Count)];
                note.Location = new Point(_random.Next(size.Width - note.Width), _random.Next(size.Height - note.Height));

                Controls.Add(note);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddNotes();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
