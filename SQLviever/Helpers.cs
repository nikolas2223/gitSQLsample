using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace SQLviever
{

    class Helpers
    {

    }

    public class Connection
    {
       private NpgsqlConnection connect = new NpgsqlConnection("Server=127.0.0.1;Port=5439;User Id=postgres;Password=candMot8;Database=postgres;");
       
       public Connection()
        {
            //connect = new NpgsqlConnection("Server=127.0.0.1;Port=5439;User Id=postgres;Password=candMot8;Database=postgres;");
            connect.Open();
        }

        public NpgsqlDataReader getSells()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT \"Sells\".id_sells, \"Sells\".cound, \"Sells\".date_sell, (\"Client\".id_client || ', ' || \"Client\".\"name\" || ' ' || \"Client\".\"familyName\"), \"Sells\".date_delivery " 
                + "FROM \"Sells\", \"Client\" WHERE \"Sells\".id_client = \"Client\".id_client", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public NpgsqlDataReader getProducts()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("select \"Product\".id_product, \"Product\".\"name\",\"Product\".price,\"Product\".\"isExist\",(\"Type\".id_type || '. ' || \"Type\".description), \"Type\".\"type\", \"Product\".count from \"Product\",\"Type\" WHERE \"Product\".id_type = \"Type\".id_type order by id_product", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public NpgsqlDataReader getClients()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"Client\"", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public NpgsqlDataReader getSelProd()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("select \"Sells-Product\".id, \"Sells-Product\".id_sells,(\"Product\".id_product || '. ' || \"Product\".\"name\" ||' '||\"Product\".\"price\") from \"Sells-Product\",\"Product\" WHERE \"Product\".id_product = \"Sells-Product\".id_product order by id", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();

            return dr;
        }

        public NpgsqlDataReader getType()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"Type\"", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public void InsertClients(string fName, string adress, string phone, bool regular, string name)
        {
            fName = ToBaStr(fName);
            adress = ToBaStr(adress);
            phone = ToBaStr(phone);
            name = ToBaStr(name);
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"Client\" (\"familyName\", adress, phone, \"isRegular\", \"name\") values ("+fName+","+adress+","+phone+","+regular+","+name+")", connect);
            cmd.ExecuteReader();
        }
        public void InsertType(string decr,bool tp)
        {
            decr = ToBaStr(decr);
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"Type\" (\"description\", type) values (" + decr + "," + tp + ")", connect);
            cmd.ExecuteReader();
        }
        public void InsertProduct(string name, string price, bool isExist, int id_type, int count)
        {
            name = ToBaStr(name);
            price = ToBaStr(price);
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"Product\" (\"name\", price, \"isExist\", id_type, count) values (" + name + "," + price + "," + isExist + "," + id_type + "," + count + ")", connect);
            cmd.ExecuteReader();
        }
        public void InsertSells(int count, DateTime date_sell, int client_id, DateTime delivery) 
        {
            string p = ToBaStr(Convert.ToString(date_sell.Date));
            string g = ToBaStr(Convert.ToString(delivery.Date));
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"Sells\" (date_sell, id_client, date_delivery,cound) values (" + p + "," + client_id + "," + g + ","+ count+")", connect);
            cmd.ExecuteReader();
        }
        public void InsertSells(int count, DateTime date_sell, int client_id)
        {
            string p = ToBaStr(Convert.ToString(date_sell.Date));
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"Sells\" (date_sell, id_client, cound) values (" + p + "," + client_id + "," + count + ")", connect);
            cmd.ExecuteReader();
        }
        public void InsertSells(DateTime date_sell, int client_id, DateTime delivery)
        {
            string p = ToBaStr(Convert.ToString(date_sell.Date));
            string g = ToBaStr(Convert.ToString(delivery.Date));
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"Sells\" (date_sell, id_client, date_delivery) values (" + p + "," + client_id + "," + g + ")", connect);
            cmd.ExecuteReader();
        }
        public void InsertSells(DateTime date_sell, int client_id)
        {
            string p = ToBaStr(Convert.ToString(date_sell.Date));
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO \"Sells\" (date_sell, id_client) values (" + p + "," + client_id + ")", connect);
            cmd.ExecuteReader();
        }
        private string ToBaStr(string x)
        {
            x = "'" + x + "'";
            return x;
        }
        public void Close()
        {
            connect.Close();
        }
    }
}
