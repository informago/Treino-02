namespace Treino_REST_02.Models
{
    public class B365API
    {
    }

    public class Evento
    {
        public int success { get; set; }
        public Pager pager { get; set; }
        public Result[] results { get; set; }
    }

    public class Pager
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int total { get; set; }
    }

    public class Result
    {
        public string id { get; set; }
        public string sport_id { get; set; }
        public string time { get; set; }
        public string time_status { get; set; }
        public Liga league { get; set; }
        public Time home { get; set; }
        public Time away { get; set; }
        public object ss { get; set; }
        public string bet365_id { get; set; }
        public Time o_home { get; set; }
        public Time o_away { get; set; }
    }

    public class Liga
    {
        public string id { get; set; }
        public string name { get; set; }
        public string cc { get; set; }
    }

    public class Time
    {
        public string id { get; set; }
        public string name { get; set; }
        public string image_id { get; set; }
        public string cc { get; set; }
    }

}
