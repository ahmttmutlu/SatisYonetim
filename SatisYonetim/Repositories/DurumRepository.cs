using SatisYonetim.Models;
using System.Data.SqlClient;

namespace SatisYonetim.Repositories
{
    public class DurumRepository
    {
        private readonly string _connectionString;
        public DurumRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<Durum> GetAllDurum()
        {
            List<Durum> durumlar = new List<Durum>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Durumlar ORDER BY DurumSira ASC ", conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            durumlar.Add(new Durum()
                            {
                                DurumId = (int)dr["DurumId"],
                                DurumAdi = (string)dr["DurumAdi"],
                                DurumSira = (int)dr["DurumSira"]
                            });
                        }
                    }
                }
            }
            return durumlar;
        }
        public Durum GetDurumById(int DurumId)
        {
            Durum durum = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Durumlar WHERE DurumId=@DurumId", conn))
                {
                    cmd.Parameters.AddWithValue("@DurumId", DurumId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new Durum
                            {
                                DurumId = (int)dr["DurumId"],
                                DurumAdi = (string)dr["DurumAdi"],
                                DurumSira = (int)dr["DurumSira"]
                            };
                        }
                    }
                }
            }
            return durum;
        }

        public void Create(Durum durum)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Durumlar (DurumAdi,DurumSira) VALUES (@DurumAdi,@DurumSira)", conn))
                {
                    cmd.Parameters.AddWithValue("@DurumAdi", durum.DurumAdi);
                    cmd.Parameters.AddWithValue("@DurumSira", durum.DurumSira);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Update(Durum durum)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("UPDATE Durumlar SET DurumAdi=@DurumAdi,DurumSira=@DurumSira WHERE DurumId=@DurumId", conn))
                {
                    cmd.Parameters.AddWithValue("@DurumId", durum.DurumId);
                    cmd.Parameters.AddWithValue("@DurumAdi", durum.DurumAdi);
                    cmd.Parameters.AddWithValue("@DurumSira", durum.DurumSira);
                    cmd.ExecuteNonQuery();
                }
            }
        }
        public void Delete(Durum durum)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                List<int> DurumIds = new List<int>();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Teklifler WHERE DurumId=@DurumId", conn))
                {
                    cmd.Parameters.AddWithValue("@DurumId", durum.DurumId);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            DurumIds.Add((int)dr["DurumId"]);
                        }
                    }
                }
                foreach (var DurumId in DurumIds)
                {
                    using (SqlCommand cmdDltTek = new SqlCommand("DELETE  FROM Teklifler WHERE DurumId=@DurumId", conn))
                    {
                        cmdDltTek.Parameters.AddWithValue("@DurumId", durum.DurumId);
                        cmdDltTek.ExecuteNonQuery();
                    }
                }
                using (SqlCommand cmdDurDltTable = new SqlCommand("DELETE FROM Durumlar WHERE DurumId=@DurumId", conn))
                {
                    cmdDurDltTable.Parameters.AddWithValue("@DurumId", durum.DurumId);
                    cmdDurDltTable.ExecuteNonQuery();
                }
            }
        }

    }
}
