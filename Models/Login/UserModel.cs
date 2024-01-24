using MySqlConnector;

namespace DotNetProject1.Models.Login
{
    public class UserModel
    {
        public uint User_seq { get; set; }
        public string User_name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        // 단방향 암호화
        public void ConvertPassword()
        {
            // System.Security.Cryptography 네임스페이스 안에 있는 HMACSHA512 클래스의 새 인스턴스를 생성
            var sha = new System.Security.Cryptography.HMACSHA512();

            // UTF-8 바이트 배열로 인코딩하여 HMAC의 비밀 키로 설정
            sha.Key = System.Text.Encoding.UTF8.GetBytes(this.Password.Length.ToString());

            // Password를 바이트 배열로 인코딩한 후 해시
            var hash = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(this.Password));

            // password를 해시값으로 변경
            this.Password = System.Convert.ToBase64String(hash);
        }

        internal int Register()
        {
            string sql = @"INSERT INTO t_user ( user_name, email, password ) SELECT @user_name, @email, @password";

            using (var connect = new MySqlConnection("Server=127.0.0.1;Port=3306;Database=myweb;Uid=root;Pwd=yein0916;\r\n"))
            {
                connect.Open();

                //  Execute(): SQL 쿼리를 실행하고, 영향을 받은 행의 수를 반환. (this)객체의 프로퍼티와 자동으로 매핑.
                return Dapper.SqlMapper.Execute(connect, sql, this);
            }
        }

    }
}
