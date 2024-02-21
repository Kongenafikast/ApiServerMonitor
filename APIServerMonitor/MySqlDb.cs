using APIServerMonitor.Models;
using MySqlConnector;

namespace APIServerMonitor
{
    public class MySqlDb
    {
        MySqlConnection connection = new("Server=192.168.0.122;Port=3306;Database=ServerMonitor;UId=teamUser;Pwd=Password1;");
        public MySqlDb()
        {

        }
        private bool OpenConn()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fejl ved åbning af forbindelse: {ex.Message}");
                return false;
            }
        }

        private void CloseConn()
        {
            try
            {
                connection.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Fejl ved lukning af forbindelse: {ex.Message}");
            }
        }
        public async Task<List<SensorData>> GetData(string query)
        {
            bool toRunOrNotToRun = OpenConn();
            if (toRunOrNotToRun)
            {
                List<SensorData> result = new();
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            SensorData sensorData = new() { 
                                Created = DateTime.Parse(reader["created"].ToString()), 
                                GroupId = int.Parse(reader["groupid"].ToString()), 
                                TypeId = int.Parse(reader["typeId"].ToString()), 
                                Id = int.Parse(reader["id"].ToString()), 
                                Value = decimal.Parse(reader["dataValue"].ToString()) };
                            result.Add(sensorData);
                        }
                    }
                }
                CloseConn();
                return result;
            }
            return null;
        }
        public async Task<bool> PostData(string query)
        {
            bool toRunOrNotToRun = OpenConn();
            if (toRunOrNotToRun)
            {
                MySqlCommand command = new MySqlCommand(query,connection);

                int test = command.ExecuteNonQuery();
                if (test != 0)
                {
                    CloseConn();
                    return true;
                }
                else
                {
                    CloseConn();
                    return false;
                }
            }
            return false;
        }

    }
}
