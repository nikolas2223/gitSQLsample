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

    public class Sell
    {
        public int ID { get; set; }
        public int COUNT { get; set; }
        public DateTime DATE_SELL { get; set; }
        public string CLIENT { get; set; }
        public object DateDelivery { get; set; }
    }
    public class Product
    {
        public int ID { get; set; }
        public object NAME { get; set; }
        public object PRICE { get; set; }
        public bool IS_EXIST { get; set; }
        public object TYPE { get; set; }
        public bool Z_TYPE { get; set; }
        public object COUNT { get; set; }
    }
    public class Client
    {
        public int ID { get; set; }
        public object NAME { get; set; }
        public object FAMILYNAME { get; set; }
        public object ADRESS { get; set; }
        public object PHONE { get; set; }
        public bool IS_REGULAR { get; set; }
    }
    public class SelProd
    {
        public int ID { get; set; }
        public object SELL_NUMBER { get; set; }
        public object PRODUCT { get; set; }
    }
    public class Type
    {
        public int ID { get; set; }
        public object DESRIPTION { get; set; }
        public bool Z_TYPE { get; set; }
    }
    public class Connection
    {
       private NpgsqlConnection connect = new NpgsqlConnection("Server=127.0.0.1;Port=5439;User Id=postgres;Password=candMot8;Database=postgres;");
       
       public Connection()
        {
            connect.Open();
        }

        public List<Sell> getSells()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT \"Sells\".id_sells, \"Sells\".cound, \"Sells\".date_sell, (\"Client\".id_client || ', ' || \"Client\".\"name\" || ' ' || \"Client\".\"familyName\"), \"Sells\".date_delivery " 
                + "FROM \"Sells\", \"Client\" WHERE \"Sells\".id_client = \"Client\".id_client", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            List<Sell> sells = new List<Sell>();
            while (dr.Read())
            {
                sells.Add(new Sell { ID = Convert.ToInt32(dr[0]), COUNT = Convert.ToInt32(dr[1]), DATE_SELL = Convert.ToDateTime(dr[2]), CLIENT = Convert.ToString(dr[3]), DateDelivery = dr[4] });
            }

            return sells;
        }

        public List<Product> getProducts()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("select \"Product\".id_product, \"Product\".\"name\",\"Product\".price,\"Product\".\"isExist\",(\"Type\".id_type || '. ' || \"Type\".description), \"Type\".\"type\", \"Product\".count from \"Product\",\"Type\" WHERE \"Product\".id_type = \"Type\".id_type order by id_product", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            List<Product> products = new List<Product>();
            while (dr.Read())
            {
                products.Add(new Product {ID = (int)(dr[0]), NAME = dr[1], PRICE = dr[2], IS_EXIST=(bool)dr[3], TYPE=dr[4], Z_TYPE=(bool)dr[5],COUNT=dr[6]});
            }
            return products;
        }

        public List<Client> getClients()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"Client\"", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            List<Client> clients = new List<Client>();
            while (dr.Read())
            {
                clients.Add(new Client { ID = (int)dr[4], NAME = dr[5], FAMILYNAME = dr[0], IS_REGULAR = (bool)dr[3], ADRESS = dr[1], PHONE = dr[2] });
            }
            return clients;
        }

        public List<SelProd> getSelProd()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("select \"Sells-Product\".id, \"Sells-Product\".id_sells,(\"Product\".id_product || '. ' || \"Product\".\"name\" ||' '||\"Product\".\"price\") from \"Sells-Product\",\"Product\" WHERE \"Product\".id_product = \"Sells-Product\".id_product order by id", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            List<SelProd> selprods = new List<SelProd>();
            while (dr.Read())
            {
                selprods.Add(new SelProd { ID = (int)dr[0], SELL_NUMBER = dr[1], PRODUCT = dr[2] });
            }
            return selprods;
        }

        public List<Type> getType()
        {
            NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM \"Type\"", connect);
            NpgsqlDataReader dr = cmd.ExecuteReader();
            List<Type> types = new List<Type>();
            while (dr.Read())
            {
                types.Add(new Type { ID = (int)dr[0], DESRIPTION = dr[1], Z_TYPE = (bool)dr[2] });
            }
            return types;
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
        public void delSells(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from \"Sells\" where id_sells = "+id, connect);
            cmd.ExecuteReader();
        }
        public void delClients(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from \"Client\" where id_client = " + id, connect);
            cmd.ExecuteReader();
        }
        public void delProducts(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from \"Product\" where id_product = " + id, connect);
            cmd.ExecuteReader();
        }
        public void delSelProd(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from \"Sells-Product\" where id = " + id, connect);
            cmd.ExecuteReader();
        }
        public void delType(int id)
        {
            NpgsqlCommand cmd = new NpgsqlCommand("delete from \"Type\" where id_type = " + id, connect);
            cmd.ExecuteReader();
        }
        public void UpdateSells(int count, DateTime date_sell, int client_id, DateTime delivery,int ID)
        {
            string p = ToBaStr(Convert.ToString(date_sell.Date));
            string g = ToBaStr(Convert.ToString(delivery.Date));
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE \"Sells\" SET date_sell = "+p+", id_client = "+client_id+", date_delivery = "+g+",cound = "+count+" WHERE id_sells = "+ID, connect);
            cmd.ExecuteReader();
        }
        public void UpdateSells(DateTime date_sell, int client_id, int ID)
        {
            string p = ToBaStr(Convert.ToString(date_sell.Date));
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE \"Sells\" SET date_sell = " + p + ", id_client = " + client_id + " WHERE id_sells = " + ID, connect);
            cmd.ExecuteReader();
        }
        public void UpdateSells(int count, DateTime date_sell, int client_id, int ID)
        {
            string p = ToBaStr(Convert.ToString(date_sell.Date));
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE \"Sells\" SET date_sell = " + p + ", id_client = " + client_id + ", cound = " + count + " WHERE id_sells = " + ID, connect);
            cmd.ExecuteReader();
        }
        public void UpdateSells(DateTime date_sell, int client_id, DateTime delivery, int ID)
        {
            string p = ToBaStr(Convert.ToString(date_sell.Date));
            string g = ToBaStr(Convert.ToString(delivery.Date));
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE \"Sells\" SET date_sell = " + p + ", id_client = " + client_id + ", date_delivery = " + g + " WHERE id_sells = " + ID, connect);
            cmd.ExecuteReader();
        }
        public void UpdateProduct(string name, string price, bool isExist, int id_type, int count,int ID)
        {
            name = ToBaStr(name);
            price = ToBaStr(price);
            NpgsqlCommand cmd = new NpgsqlCommand("UPDATE \"Product\" SET \"name\" = "+name+", price = "+price+", \"isExist\" = "+isExist+", id_type = "+id_type+", count = "+count+" WHERE id_product = "+ID, connect);
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
