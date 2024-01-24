using MySqlConnector;

namespace DotNetProject1.Models
{
    public class TicketModel
    {
        public int Ticket_id { get; set; }
        public string Title { get; set; }
        public string Statuss { get; set; }

        public static List<TicketModel> GetList()
        {
            // db ¿¬°á
            using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
            {
                connect.Open();

                string sql = "SELECT * FROM t_ticket";
               
                return Dapper.SqlMapper.Query<TicketModel>(connect, sql).ToList();
            }
        }

        public int Update()
        {
            string sql = @"
UPDATE t_ticket
SET
    title = @title
WHERE
	ticket_id = @ticket_id
";
            using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
            {
                connect.Open();

                return Dapper.SqlMapper.Execute(connect, sql, this);
            }
        }
    }
}


