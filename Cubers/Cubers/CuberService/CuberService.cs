using Cubers.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cubers
{
    public class CuberService : ICuberService
    {
        public string ConnectionString;

        public CuberService(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// Gets a cuber, and its solves and metatata
        /// </summary>
        /// <param name="id">The cuber's id</param>
        /// <returns></returns>
        public Cuber GetCuber(int id)
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();

            // get name
            var getCuberQuery = "Select name FROM cuber WHERE id = @id";
            var getCuberCmd = new MySqlCommand(getCuberQuery, conn);
            getCuberCmd.Parameters.AddWithValue("@id", id);
            var cuberReader = getCuberCmd.ExecuteReader();
            cuberReader.Read();
            var cuber = new Cuber() { Name = cuberReader[0].ToString(), Id = id };
            cuberReader.Close();

            // get solves
            var getSolvesQuery = @"SELECT event, time FROM solve where cuber_id = @id";
            var getSolvesCmd = new MySqlCommand(getSolvesQuery, conn);
            getSolvesCmd.Parameters.AddWithValue("@id", id);
            var solvesReader = getSolvesCmd.ExecuteReader();
            while (solvesReader.Read())
            {
                
                var evt = solvesReader[0].ToString();
                var time = Math.Round(Convert.ToDouble(solvesReader[1]) * 100) / 100;
                if (evt.Equals("3x3"))
                    cuber.Solves3x3.Add(time);
                else if (evt.Equals("OH"))
                    cuber.SolvesOh.Add(time);
                else if (evt.Equals("4x4"))
                    cuber.Solves4x4.Add(time);
            }
            solvesReader.Close();

            // get metadata
            var metadataQuery = @"SELECT mkey, mvalue FROM metadata WHERE cuber_id = @id";
            var metadataCmd = new MySqlCommand(metadataQuery, conn);
            metadataCmd.Parameters.AddWithValue("@id", id);
            var metadataRader = metadataCmd.ExecuteReader();
            while (metadataRader.Read())
            {
                cuber.Metadata.Add(new CuberMetadata { Key = metadataRader[0].ToString(), Value = metadataRader[1].ToString() });
            }
            metadataRader.Close();

            conn.Close();
            return cuber;
        }

        /// <summary>
        /// Gets a summary containing each cuber's fastest times and leaders for each event
        /// Pbs for evnts where a cuber has no solves are -1
        /// </summary>
        /// <returns></returns>
        public CuberSummary GetCuberSummary()
        {
            // get fastest times for each cuber
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            var cuberQuery = @"SELECT cuber.name, 
MIN(IF(solve.event = '3x3', solve.time, null)),
MIN(IF(solve.event = 'OH', solve.time, null)),
MIN(IF(solve.event = '4x4', solve.time, null)),
cuber.id
FROM cuber LEFT OUTER JOIN solve ON cuber.id = solve.cuber_id
GROUP BY cuber.id";
            var cuberCmd = new MySqlCommand(cuberQuery, conn);
            var cuberReader = cuberCmd.ExecuteReader();
            List<CuberSummaryEntry> entries = new List<CuberSummaryEntry>();
            while (cuberReader.Read())
            {
                var pb3x3 = cuberReader[1] != DBNull.Value? Math.Round(Convert.ToDouble(cuberReader[1]) * 100) / 100 : -1;
                var pbOh = cuberReader[2] != DBNull.Value? Math.Round(Convert.ToDouble(cuberReader[2]) * 100) / 100 : -1;
                var pb4x4 = cuberReader[3] != DBNull.Value? Math.Round(Convert.ToDouble(cuberReader[3]) * 100) / 100 : -1;
                entries.Add(new CuberSummaryEntry
                {
                    CuberName = cuberReader[0].ToString(),
                    Pb3x3 = pb3x3,
                    PbOh = pbOh,
                    Pb4x4 = pb4x4,
                    CuberId = Convert.ToInt32(cuberReader[4])
                });
            }
            cuberReader.Close();

            // get leaders for each event
            var leaderQuery = @"select event, name, solve1.time
from cuber join solve as solve1 on cuber.id = solve1.cuber_id
where solve1.time = (select min(b.time) from solve b where b.event = solve1.event)
group by event";
            var leaderCmd = new MySqlCommand(leaderQuery, conn);
            var leaderReader = leaderCmd.ExecuteReader();
            var leaders = new CuberSummaryLeaders();
            while (leaderReader.Read())
            {
                var evt = leaderReader[0].ToString();
                if (evt.Equals("3x3"))
                {
                    leaders.Name3x3 = leaderReader[1].ToString();
                    leaders.Time3x3 = Math.Round(Convert.ToDouble(leaderReader[2]) * 100) / 100;
                }
                else if (evt.Equals("OH"))
                {
                    leaders.NameOh = leaderReader[1].ToString();
                    leaders.TimeOh = Math.Round(Convert.ToDouble(leaderReader[2]) * 100) / 100;
                }
                else if (evt.Equals("4x4"))
                {
                    leaders.Name4x4 = leaderReader[1].ToString();
                    leaders.Time4x4 = Math.Round(Convert.ToDouble(leaderReader[2]) * 100) / 100;
                }
            }
            conn.Close();

            return new CuberSummary { Cubers = entries, Leaders = leaders };
        }

        /// <summary>
        /// Adds a new cuber to the database
        /// </summary>
        /// <param name="cuber">The cuber to add to the database</param>
        public void AddCuber(Cuber cuber)
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            var query = @"INSERT INTO Cuber (name) VALUES (@name)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", cuber.Name);
            cmd.ExecuteNonQuery();
            int id = (int)(cmd.LastInsertedId);
            cuber.Id = id;
            UpdateSolves(cuber, conn);
            UpdateMetadata(cuber, conn);
            conn.Close();
        }

        /// <summary>
        /// Updates an existing cuber
        /// </summary>
        /// <param name="cuber">The cuber to update</param>
        public void SaveCuber(Cuber cuber)
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            var query = @"UPDATE cuber SET name = @name WHERE id = @id";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@name", cuber.Name);
            cmd.Parameters.AddWithValue("@id", cuber.Id);
            cmd.ExecuteNonQuery();
            UpdateSolves(cuber, conn);
            UpdateMetadata(cuber, conn);
            conn.Close();
        }

        private void UpdateSolves(Cuber cuber, MySqlConnection conn)
        {
            // remove existing solves
            var deleteQuery = @"DELETE FROM solve WHERE cuber_id = @id";
            var deleteCmd = new MySqlCommand(deleteQuery, conn);
            deleteCmd.Parameters.AddWithValue("@id", cuber.Id);
            deleteCmd.ExecuteNonQuery();

            // add current solves
            var insertQuery = @"INSERT INTO solve (event, time, cuber_id) values (@event, @time, @cuberId)";
            foreach (var time in cuber.Solves3x3)
            {
                var insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@event", "3x3");
                insertCmd.Parameters.AddWithValue("@time", time);
                insertCmd.Parameters.AddWithValue("@cuberId", cuber.Id);
                insertCmd.ExecuteNonQuery();
            }
            foreach (var time in cuber.SolvesOh)
            {
                var insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@event", "OH");
                insertCmd.Parameters.AddWithValue("@time", time);
                insertCmd.Parameters.AddWithValue("@cuberId", cuber.Id);
                insertCmd.ExecuteNonQuery();
            }
            foreach (var time in cuber.Solves4x4)
            {
                var insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@event", "4x4");
                insertCmd.Parameters.AddWithValue("@time", time);
                insertCmd.Parameters.AddWithValue("@cuberId", cuber.Id);
                insertCmd.ExecuteNonQuery();
            }
        }

        private void UpdateMetadata(Cuber cuber, MySqlConnection conn)
        {
            // delete old metadata
            var deleteQuery = @"DELETE FROM metadata WHERE cuber_id = @id";
            var deleteCmd = new MySqlCommand(deleteQuery, conn);
            deleteCmd.Parameters.AddWithValue("@id", cuber.Id);
            deleteCmd.ExecuteNonQuery();

            // add updated metadata
            var insertQuery = @"INSERT INTO metadata (mkey, mvalue, cuber_id) VALUES (@key, @value, @cuberId)";
            foreach (var meta in cuber.Metadata)
            {
                var insertCmd = new MySqlCommand(insertQuery, conn);
                insertCmd.Parameters.AddWithValue("@key", meta.Key);
                insertCmd.Parameters.AddWithValue("@value", meta.Value);
                insertCmd.Parameters.AddWithValue("@cuberId", cuber.Id);
                insertCmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Deletes a cuber along with its solves and metadata
        /// </summary>
        /// <param name="cuberId">The id of the cuber to delete</param>
        public void DeleteCuber(int cuberId)
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            var query = @"DELETE FROM cuber WHERE id = @id";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue(@"id", cuberId);
            cmd.ExecuteNonQuery();
            // and take advantage of cascade delete to remove the cuber's solves and metadata
            conn.Close();
            
        }

        /// <summary>
        /// Adds a single solve to a cuber
        /// </summary>
        /// <param name="solve"></param>
        public void AddSolve(PostedTime solve)
        {
            var conn = new MySqlConnection(ConnectionString);
            conn.Open();
            var query = @"INSERT INTO solve (time, event, cuber_id) VALUES (@time, @event, @cuberId)";
            var cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@cuberId", solve.CuberId);
            cmd.Parameters.AddWithValue("@time", solve.Time);
            cmd.Parameters.AddWithValue("@event", solve.Event);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
