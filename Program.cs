using System;
using System.Data.SqlClient;
using System.Data;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;

namespace SqlConnection_and_Json
{
    class Program
    {
        
            public static void Read()
            {
            int Number = 1;
            Crud oCrud = new Crud();
            var ListPeople = oCrud.Get();

            foreach (var List in ListPeople)
            {
                
                Console.WriteLine( "#" + (Number ++) +"  FirstName: {0}\n Last Name: {1}\n Age: {2}\n Number: {3}", List.FirstName, List.LastName, List.Age, List.Id);
                Console.WriteLine("*************************************");
            }
            
            }

        static void Main(string[] args)
        {
            //Object serialization and JSON deserialization
            PersonBD oPersonJson = new PersonBD("Dewry", "Peña", 24, 1);

            //once these sentences are executed you will be able to find the generated txt in the bin/debug/% folder
            string myJson = JsonSerializer.Serialize(oPersonJson);
            File.WriteAllText("MydataInJson.txt", myJson);
            PersonBD oPersonJsonSerealize = JsonSerializer.Deserialize<PersonBD>(myJson);

            //deserealize
          /*string myJson = File.ReadAllText("MyDataInJson.txt")
           * 
           * 
           */

            Crud oCrud = new Crud();
            string answer = null;
            string n = null;
            int x = 0;
            
            Console.WriteLine("***************************** Hello World!**************************");
            Console.WriteLine("*****************************Data of People*************************!\n");
            Console.WriteLine("*****************************Select a Number*************************!\n");
            Console.WriteLine("1.Read");
            Console.WriteLine("2.Create");
            Console.WriteLine("3.Update");
            Console.WriteLine("4.Delete\n");

            byte menu = Convert.ToByte(Console.ReadLine());
            switch (menu)
            {
                case 1: Read(); break;
                case 2:
                    Read();
                    Console.WriteLine("*************************do you want to insert people?   Press s / n*****************************");
                    answer = Console.ReadLine();
                    if (answer.ToUpper().Equals("S"))
                    {



                        Console.WriteLine("*************************Insert People*****************************");
                        Console.WriteLine("*************************How many people do you want to insert?*********************");
                        n = Console.ReadLine();

                        while (x < Convert.ToInt32(n))
                        {
                            x++;
                            Console.WriteLine("*************************Insert a new person**********************************\n");
                            Console.WriteLine("*************************Insert First Name: ");
                            string FirstName = Console.ReadLine();
                            Console.WriteLine("*************************Insert Last Name: ");
                            string LastName = Console.ReadLine();

                            Console.Write("*************************Insert age: ");
                            string Age = Console.ReadLine();
                            int Id = 0;

                            PersonBD oPeople = new PersonBD(FirstName, LastName, Convert.ToInt32(Age), Id);
                            oCrud.Add(oPeople);

                        }

                        Read();
                        Console.WriteLine("*******************THANK YOU FOR USING THE APPLICATION!!!!****************************");
                    }
                    else
                    {
                        Console.WriteLine("*******************THANK YOU FOR USING THE APPLICATION!!!!****************************");
                    }
                    break;
                case 3: Console.WriteLine("*******************************you can select the person to edit through their number*********************************");
                    Read();
                    {
                        Console.WriteLine("*************************Insert First Name: ");
                        string FirstName = Console.ReadLine();
                        Console.WriteLine("*************************Insert Last Name: ");
                        string LastName = Console.ReadLine();

                        Console.Write("*************************Insert age: ");
                        string Age = Console.ReadLine();
                        Console.WriteLine("enter the number of the person to edit");
                        int Id = Convert.ToInt32(Console.ReadLine());

                        PersonBD oPeopleedit = new PersonBD(FirstName, LastName, Convert.ToInt32(Age), Id);
                        oCrud.Edit(oPeopleedit);
                        Console.WriteLine($"*************************The person's {Id} details have been updated***********************\n");
                        Read();
                        Console.WriteLine("*******************THANK YOU FOR USING THE APPLICATION!!!!****************************");
                    }
                    break;
                case 4:
                    Read();
                    Console.WriteLine("******************************Enter the number of the user you want to delete****************************\n");
                    {
                        int Id = Convert.ToInt32(Console.ReadLine());

                        PersonBD oPeopleedit = new PersonBD("Removed", "Removed", 0, Id);
                        oCrud.Edit(oPeopleedit);
                        Console.WriteLine($"*************************The person's {Id} details have been deleted***********************\n");
                        Read();
                        Console.WriteLine("*******************THANK YOU FOR USING THE APPLICATION!!!!****************************");
                    }
                    break;
                default: Console.WriteLine("******************************Restart the program and use a correct option*******************************"); break;

            }
            
            
            
         
                Console.WriteLine("*******************THANK YOU FOR USING THE APPLICATION!!!!****************************");
            
            Console.ReadKey();
        }
       class Connection
        {
            public static string ConnectionString = "Data Source=DESKTOP-HN2CEKL\\SQLEXPRESS;Initial Catalog=Person;Integrated Security=True";

          public static SqlConnection Conect = new SqlConnection(ConnectionString);
            public SqlConnection OpenConnect()
            {
                if(Conect.State == ConnectionState.Closed)
                {
                    Conect.Open();
                }
                return Conect;
            }
            public SqlConnection CloseConnect()
            {
                if (Conect.State == ConnectionState.Open)
                {
                    Conect.Close();
                }
                return Conect;
            }

        }
        class Crud
        {
            Connection oConn = new Connection();
            public List<PersonBD> Get()
            {
                List<PersonBD> DataPeopleList = new List<PersonBD>();

                string query = "Select FirstName, LastName, Age, Id as Number from DataPerson";

                using(var cmd = new SqlCommand(query, Connection.Conect))
                {


                    oConn.OpenConnect();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while(reader.Read())
                    {
                        string FirstName = reader.GetString(0);
                        string LastName = reader.GetString(1);
                        int Age = reader.GetInt32(2);
                        int Id = reader.GetInt32(3);

                        PersonBD oPerson = new PersonBD(FirstName, LastName, Age, Id);

                        DataPeopleList.Add(oPerson);
                    }

                    reader.Close();
                    oConn.CloseConnect();
                }

                return DataPeopleList;
            }
            public void Add(PersonBD oPeople)
            {
                string QueryInsert = "insert into DataPerson(FirstName,LastName,Age) "+
                    "values(@FirstName,@LastName,@Age)";
                using (var command = new SqlCommand(QueryInsert, Connection.Conect))
                {
                    
                    command.Parameters.AddWithValue("@FirstName", oPeople.FirstName);
                    command.Parameters.AddWithValue("@LastName", oPeople.LastName);
                    command.Parameters.AddWithValue("@Age", oPeople.Age);

                    oConn.OpenConnect();
                    command.ExecuteNonQuery();
                    oConn.CloseConnect();
                }
            }
            public void Edit(PersonBD oPeople)
            {
                string QueryEdit = "update DataPerson set FirstName=@FirstName,LastName=@LastName,Age=@Age "+
                    "where Id=@Id";
                using (var command = new SqlCommand(QueryEdit, Connection.Conect))
                {
                    
                    command.Parameters.AddWithValue("@FirstName", oPeople.FirstName);
                    command.Parameters.AddWithValue("@LastName", oPeople.LastName);
                    command.Parameters.AddWithValue("@Age", oPeople.Age);
                    command.Parameters.AddWithValue("@Id", oPeople.Id);

                    oConn.OpenConnect();
                    command.ExecuteNonQuery();
                    oConn.CloseConnect();
                }

            }
            public void Delete(PersonBD oPeople)
            {
                string QueryDelete = "Delete from DataPerson where Id=@Id";
                using (var command = new SqlCommand(QueryDelete, Connection.Conect))
                {
                    
       
                    command.Parameters.AddWithValue("@Id", oPeople.Id);

                    oConn.OpenConnect();
                    command.ExecuteNonQuery();
                    oConn.CloseConnect();
                }

            }
        }
        class PersonBD
        {
            private int _Id;
            private string _FirstName;
            private string _LastName;
            
            private int _Age;

            public string FirstName { get => _FirstName; set => _FirstName = value; }
            public string LastName { get => _LastName; set => _LastName = value; }
            public int Age { get => _Age; set => _Age = value; }
            public int Id { get => _Id; set => _Id = value; }

            public PersonBD(string FirstName, string LastName, int Age, int Id)
            {
                this.FirstName = FirstName;
                this.LastName = LastName;
                this.Age = Age;
                this.Id = Id;
            }
        }
    }
}
