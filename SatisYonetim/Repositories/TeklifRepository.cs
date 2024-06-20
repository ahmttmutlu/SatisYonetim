
using SatisYonetim.Models;
using System.Data.SqlClient;

namespace SatisYonetim.Repositories
{
    public class TeklifRepository
    {
        private readonly string _connectionString;
        public TeklifRepository(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<DurumViewModel> GetAllTeklif()
        {
            List<DurumViewModel> teklifler = new List<DurumViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT D.DurumId AS dDurumId, D.DurumAdi AS dDurumAdi, T.TeklifId AS tTeklifId, T.TeklifAdi AS tTeklifAdi, T.MusteriAdi AS tMusteriAdi, T.Fiyat AS tFiyat FROM Durumlar AS D JOIN Teklifler AS T ON D.DurumId = T.DurumId", conn))
                {
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            teklifler.Add(new DurumViewModel
                            {
                                DurumId = (int)dr["dDurumId"],
                                DurumAdi = (string)dr["dDurumAdi"],
                                TeklifId = (int)dr["tTeklifId"],
                                TeklifAdi = (string)dr["tTeklifAdi"],
                                MusteriAdi = (string)dr["tMusteriAdi"],
                                Fiyat = (int)dr["tFiyat"]
                            });
                        }
                    }
                }
            }
            return teklifler;
        }
        public DurumViewModel GetTeklifById(int Id)
        {
            DurumViewModel teklif = null;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Teklifler WHERE TeklifId=@TeklifId", conn))
                {
                    cmd.Parameters.AddWithValue("@TeklifId", Id);
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            return new DurumViewModel
                            {

                                TeklifId = (int)dr["TeklifId"],
                                TeklifAdi = (string)dr["TeklifAdi"],
                                MusteriAdi = (string)dr["MusteriAdi"],
                                Fiyat = (int)dr["Fiyat"],
                                DurumId = (int)dr["DurumId"]
                            };
                        }
                    }
                }
            }
            return teklif;
        }

        public void Create(DurumViewModel teklif)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();


                string DurumAdi = teklif.DurumAdi;
                using (SqlCommand cmd = new SqlCommand("SELECT DurumId FROM Durumlar WHERE DurumAdi = @DurumAdi", connection))
                {
                    cmd.Parameters.AddWithValue("@DurumAdi", DurumAdi);
                    cmd.ExecuteNonQuery();

                    using (SqlCommand cmdd = new SqlCommand("INSERT INTO Teklifler (TeklifAdi, MusteriAdi, Fiyat, DurumId) VALUES (@TeklifAdi, @MusteriAdi, @Fiyat, @DurumId)", connection))
                    {
                        cmdd.Parameters.AddWithValue("@TeklifAdi", teklif.TeklifAdi);
                        cmdd.Parameters.AddWithValue("@MusteriAdi", teklif.MusteriAdi);
                        cmdd.Parameters.AddWithValue("@Fiyat", teklif.Fiyat);
                        cmdd.Parameters.AddWithValue("@DurumId", DurumAdi);
                        cmdd.ExecuteNonQuery();
                    }
                }
            }
        }
        public void Update(DurumViewModel teklif)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string durumAdi = teklif.DurumAdi;
                using (SqlCommand cmd = new SqlCommand("SELECT DurumId FROM Durumlar WHERE DurumAdi = @DurumAdi", connection))
                {
                    cmd.Parameters.AddWithValue("@DurumAdi", durumAdi);
                    cmd.ExecuteNonQuery();
                }

                using (SqlCommand cmd = new SqlCommand("UPDATE Teklifler SET TeklifAdi = @TeklifAdi, MusteriAdi = @MusteriAdi, Fiyat = @Fiyat, DurumId = @DurumId WHERE TeklifId = @TeklifId", connection))
                {
                    cmd.Parameters.AddWithValue("@TeklifId", teklif.TeklifId);
                    cmd.Parameters.AddWithValue("@TeklifAdi", teklif.TeklifAdi);
                    cmd.Parameters.AddWithValue("@MusteriAdi", teklif.MusteriAdi);
                    cmd.Parameters.AddWithValue("@Fiyat", teklif.Fiyat);
                    cmd.Parameters.AddWithValue("@DurumId", durumAdi);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Delete(DurumViewModel teklif)
        {
            using (SqlConnection con = new SqlConnection(_connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Teklifler WHERE TeklifId=@TeklifId", con))
                {
                    cmd.Parameters.AddWithValue("@TeklifId", teklif.TeklifId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
