using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace ArticleHelper250418
{
    public class DatabaseFunctions:DatabaseConnectionString
    {
        public bool titleSaveToDatabase(string titleString)
        {
            bool isTitleExistAlready = IsTitleExist(titleString);
            if (isTitleExistAlready)
            {
                return false;
            }
            else
            {
                SqlConnection connection = new SqlConnection(connectionString);
                string query = "INSERT INTO titleTable(title) VALUES (@title)";
                SqlCommand cmd = new SqlCommand(query, connection);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@title", titleString);
                int rowAffected = 0;
                try
                {
                    connection.Open();
                    rowAffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }
                catch (Exception e)
                {

                }
                return true;
            }

        }
        public bool IsTitleExist(string titleString)
        {
            bool isTitleExists = false;

            SqlConnection connection = new SqlConnection(connectionString);

            string query = "SELECT title FROM titleTable WHERE title= @title";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();

            //            command.Parameters.Add("DeptCode", SqlDbType.NVarChar);
            //            command.Parameters["DeptCode"].Value = DeptCode;
            command.Parameters.AddWithValue("@title", titleString);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                isTitleExists = true;
            }
            connection.Close();

            return isTitleExists;
        }
        public int GetLastTitleIdInserted()
        {
            int titleId=0;
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT MAX(id) FROM titleTable";
            SqlCommand command = new SqlCommand(query, connection);

            //command.Parameters.Clear();
            //            command.Parameters.Add("DeptCode", SqlDbType.NVarChar);
            //            command.Parameters["DeptCode"].Value = DeptCode;
            //command.Parameters.AddWithValue("@title", titleString);
            connection.Open();
            //SqlDataReader reader = command.ExecuteReader();
            //if (reader.HasRows)
            //{
                //if (reader.Read())
                //{
                    //string val = (reader["id"].ToString());
            titleId = Convert.ToInt32(command.ExecuteScalar().ToString());
                    //Do your stuff here.
                //}

                 
                //titleId = Convert.ToInt32(reader["id"].ToString());
            //}
            connection.Close();
            return titleId;
        }


        public List<TitleModel> GetAllTitlesWithId()
        {
            List<TitleModel> listOfAllTitles = new List<TitleModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM titleTable";
            SqlCommand command = new SqlCommand(query, connection);

            //command.Parameters.Clear();
            //            command.Parameters.Add("DeptCode", SqlDbType.NVarChar);
            //            command.Parameters["DeptCode"].Value = DeptCode;
            //command.Parameters.AddWithValue("@title", titleString);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                TitleModel aTitleModel = new TitleModel();
                aTitleModel.titleId= Convert.ToInt32(reader["id"].ToString());
                aTitleModel.titleName = reader["title"].ToString();
                listOfAllTitles.Add(aTitleModel);
            }
            connection.Close();
            return listOfAllTitles;
        }

        public int GetArticleDataInserted(int titleId, Dictionary<string, string> articleDataList)
        {
            int rowAffected = 0;
            SqlConnection connection = new SqlConnection(connectionString);

            foreach(var a in articleDataList)
            {
                
                string query = "INSERT INTO textTable(heading,paragraph,titleId) VALUES (@heading,@paragraph,@titleId)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.Clear();
                command.Parameters.AddWithValue("@heading", a.Key.ToString());
                command.Parameters.AddWithValue("@paragraph", a.Value.ToString());
                command.Parameters.AddWithValue("@titleId", titleId);
                try
                {
                    connection.Open();
                    rowAffected = command.ExecuteNonQuery();
                    connection.Close();
                }
                catch(Exception ex)
                {
                    return rowAffected = -3;//not all saved successfully
                }
            }    
            return rowAffected;
        }


        /*public Dictionary<string, string> GetIndividualPaper(int id)
        {
            Dictionary<string, string> articleSeparateParagraphs = new Dictionary<string, string>();
            List<ArticleModel> listOfAllTitles = new List<ArticleModel>();
            SqlConnection connection = new SqlConnection(connectionString);
            string query = "SELECT * FROM textTable WHERE titleId=@titleId";
            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.Clear();
            command.Parameters.AddWithValue("@titleId", id);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {

                string heading = reader["heading"].ToString();
                string paragraph = reader["paragraph"].ToString();
                articleSeparateParagraphs.Add(heading, paragraph);
            }
            connection.Close();

            return articleSeparateParagraphs;
        }*/

        public ArticleModel GetIndividualPaper(int id)
        {
            ArticleModel aArticleModel = new ArticleModel();
            //from the titleTable
            SqlCommand command1, command2;
            SqlConnection connection = new SqlConnection(connectionString);
            string queryForTitle = "SELECT title FROM titleTable WHERE id=@id";
            command1 = new SqlCommand(queryForTitle, connection);
            command1.Parameters.Clear();
            command1.Parameters.AddWithValue("@id", id);
            connection.Open();
            string title = command1.ExecuteScalar().ToString();
            connection.Close();
            aArticleModel.articleTitle = title;
            //from the textTable
            Dictionary<string, string> articleSeparateParagraphs = new Dictionary<string, string>();
            List<ArticleModel> listOfAllTitles = new List<ArticleModel>();
            
            string queryForText = "SELECT * FROM textTable WHERE titleId=@titleId";
            command2 = new SqlCommand(queryForText, connection);

            command2.Parameters.Clear();
            command2.Parameters.AddWithValue("@titleId", id);
            connection.Open();
            SqlDataReader reader = command2.ExecuteReader();
            while (reader.Read())
            {

                string heading = reader["heading"].ToString();
                string paragraph = reader["paragraph"].ToString();
                articleSeparateParagraphs.Add(heading, paragraph);
            }
            connection.Close();
            aArticleModel.articleSeparateParagraphs = articleSeparateParagraphs;

            return aArticleModel;
        }
    }
}