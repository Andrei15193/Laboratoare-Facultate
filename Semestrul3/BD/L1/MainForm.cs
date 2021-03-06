﻿using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace CSLabBD
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void PopulateFields(Movie movie)
        {
            string[] actors = movie.Actors;
            TextBoxTitle.Text = movie.Title;
            DatePickerReleaseDate.Value = movie.ReleaseDate;
            NumericVotes.Value = movie.Votes;
            ListBoxActors.Items.Clear();
            for (int i = 0, count = actors.Length; i < count; i++)
                ListBoxActors.Items.Add(movie.Actors[i]);
        }

        private void AddMovieFromRawInput(string text)
        {
            String[] fields = text.Split(';');
            if (fields.Length == 4)
                ListBoxMovies.Items.Add(new Movie(fields[0], new DateTime(long.Parse(fields[1])), decimal.Parse(fields[2]), fields[3].Split(',')));
        }

        private void EventOpenFile(object sender, EventArgs e)
        {
            if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                EventClearAll(sender, e);
                LoadFromFile(OpenFileDialog.FileName);
            }
        }

        private void LoadFromFile(string fileName)
        {
            string line;
            System.IO.StreamReader file = new System.IO.StreamReader(fileName);
            while ((line = file.ReadLine()) != null)
                AddMovieFromRawInput(line);
            file.Close();
        }

        private void EventSaveFile(object sender, EventArgs e)
        {
            SaveFileDialog.ShowDialog();
        }

        private void EventSaveFile(object sender, CancelEventArgs e)
        {
            int countMinusOne;
            string line;
            string[] actors;
            Movie movie;
            System.IO.StreamWriter file = new System.IO.StreamWriter(SaveFileDialog.FileName);
            for (int i = 0, size = ListBoxMovies.Items.Count; i < size; i++)
            {
                movie = ListBoxMovies.Items[i] as Movie;
                actors = movie.Actors;
                countMinusOne = actors.Length - 1;
                line = movie.Title + ";" + System.Convert.ToString(movie.ReleaseDate.Ticks) + ";" + System.Convert.ToString(movie.Votes) + ";";
                if (countMinusOne != -1)
                {
                    for (int j = 0; j < countMinusOne; j++)
                        line += actors[j] + ",";
                    line += actors[countMinusOne];
                }
                file.WriteLine(line);
            }
            file.Close();
        }

        private void EventClearAll(object sender, EventArgs e)
        {
            TextBoxTitle.Text = "";
            ListBoxActors.Items.Clear();
            ListBoxMovies.Items.Clear();
            NumericVotes.Value = NumericVotes.Minimum;
        }

        private void EventAddActor(object sender, EventArgs e)
        {
            if (addActorDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                ListBoxActors.Items.Add(addActorDialog.ActorName);
        }

        private void EventCloseApplication(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EventAddMovie(object sender, EventArgs e)
        {
            string[] actors = new string[ListBoxActors.Items.Count];
            for (int i = actors.Length - 1; i >= 0; i--)
                actors[i] = ListBoxActors.Items[i].ToString();
            ListBoxMovies.SetSelected(ListBoxMovies.Items.Add(new Movie(TextBoxTitle.Text, DatePickerReleaseDate.Value, NumericVotes.Value, actors)), true);
        }

        private void EventUpdateMovie(object sender, EventArgs e)
        {
            int index = ListBoxMovies.SelectedIndex;
            Movie movie = ListBoxMovies.SelectedItem as Movie;
            if (movie != null)
            {
                string[] actors = new string[ListBoxActors.Items.Count];
                for (int i = 0; i < actors.Length; i++)
                    actors[i] = ListBoxActors.Items[i].ToString();
                movie.Title = TextBoxTitle.Text;
                movie.ReleaseDate = DatePickerReleaseDate.Value;
                movie.Votes = NumericVotes.Value;
                movie.Actors = actors;
                ListBoxMovies.Items.Insert(index, movie);
                ListBoxMovies.Items.RemoveAt(index + 1);
                ListBoxMovies.SetSelected(index, true);
            }
        }

        private void EventDeleteMovie(object sender, EventArgs e)
        {
            int index = ListBoxMovies.SelectedIndex;
            if (index != -1)
            {
                ListBoxMovies.Items.RemoveAt(index);
                TextBoxTitle.Text = "";
                ListBoxActors.Items.Clear();
                NumericVotes.Value = NumericVotes.Minimum;
            }
        }

        private void EventMovieSelect(object sender, MouseEventArgs e)
        {
            Movie movie = ListBoxMovies.SelectedItem as Movie;
            if (movie != null)
                PopulateFields(movie);
        }

        private NewActorDialog addActorDialog = new NewActorDialog();

        private void EventRemoveSelectedActor(object sender, EventArgs e)
        {
            int index = ListBoxActors.SelectedIndex;
            if (index != -1)
                ListBoxActors.Items.RemoveAt(index);
        }

        private void EventRemoveAllActors(object sender, EventArgs e)
        {
            ListBoxActors.Items.Clear();
        }
    }
}
