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
            //CONCAT('%', IFNULL(@search, ''), '%'): @search 변수 값 앞뒤로 %를 붙여, 해당 변수에 어떤 값이 들어가든 그 값이 문자열 중간에 위치하는지 확인
			 
            using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
			{
				connect.Open();
				
				var parameters = new { search = search ?? string.Empty };

				return Dapper.SqlMapper.Query<BoardModel>(connect, sql, parameters).ToList();
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


		// 제목, 내용 체크
		void CheckContents()
		{
            if (string.IsNullOrWhiteSpace(this.Title))
            {
                throw new Exception("제목을 입력해 주세요.");
            }
            if (string.IsNullOrWhiteSpace(this.Contents))
            {
                throw new Exception("내용을 입력해 주세요.");
            }
            if (string.IsNullOrWhiteSpace(this.Reg_username))
            {
                throw new Exception("작성자 이름을 입력해 주세요.");
            }
        }

		public int Insert()
		{
			CheckContents();

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
            CheckContents();

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


