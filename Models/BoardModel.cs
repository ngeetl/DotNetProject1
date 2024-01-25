using Dapper;
using MySqlConnector;

namespace DotNetProject1.Models
{
    public class BoardModel
    {
        public uint Idx { get; set; }

        public string Title { get; set; }

        public string Contents { get; set; }

        public uint Reg_user { get; set; }

        public string Reg_username { get; set; }

        public DateTime Reg_date { get; set; }

        public uint View_Cnt { get; set; }

        public short Status_flag { get; set; }

		public static List<BoardModel> GetList(string search)
		{
			string sql = @"SELECT
	A.idx
	,A.title
    ,A.reg_user
    ,A.reg_username
    ,A.reg_date
    ,A.view_Cnt
    ,A.status_flag
FROM
	t_board A
WHERE
	A.title LIKE CONCAT('%', IFNULL(@search, ''), '%')
ORDER BY
    A.idx DESC";

			using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
			{
				connect.Open();

				return Dapper.SqlMapper.Query<BoardModel>(connect, sql).ToList();
			}
		
		}

		public static BoardModel Get(uint idx)
		{
			string sql = @"
SELECT
	A.idx
	,A.title
	,A.contents
    ,A.reg_user
    ,A.reg_username
    ,A.reg_date
    ,A.view_Cnt
    ,A.status_flag
FROM
	t_board A
WHERE
	A.idx = @idx
";
			using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
			{
				connect.Open();

				return Dapper.SqlMapper.QuerySingle<BoardModel>(connect, sql);
			}
		}


		public int Insert()
		{
			string sql = @"
INSERT INTO t_board (
	title
	,contents
    ,reg_user
    ,reg_username
    ,reg_date
    ,view_Cnt
    ,status_flag
)
VALUES (
    @title
	,@contents
    ,@reg_user
    ,@reg_username
    ,now()
    ,0
    ,0
)
";
			using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
			{
				connect.Open();

				return Dapper.SqlMapper.Execute(connect, sql, this);
			}

		}

		public int Update()
		{
			string sql = @"
UPDATE t_board
SET
    title = @title
	,contents = @contents
WHERE
    idx = @idx
";
			using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
			{
				connect.Open();

				return Dapper.SqlMapper.Execute(connect, sql, this);
			}
		}

		public int Delete()
		{
			string sql = @"
DELETE FROM t_board
WHERE
    idx = @idx
";
			using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
			{
				connect.Open();

				return Dapper.SqlMapper.Execute(connect, sql, this);
			}
        }
    }
}


