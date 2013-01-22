namespace CSLabBD
{
    class Movie
    {
        public Movie(string title, System.DateTime releaseDate, decimal votes, string[] actors)
        {
            this.Title = string.Copy(title);
            this.ReleaseDate = releaseDate;
            this.Actors = new string[actors.Length];
            this.Votes = votes;
            actors.CopyTo(this.Actors, 0);
        }

        public override string ToString()
        {
            return this.Title;
        }

        public string Title { set; get; }

        public System.DateTime ReleaseDate { set; get; }

        public string[] Actors { set; get; }

        public decimal Votes { get; set; }
    }
}
