namespace LinqAndLamdaExpressions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Models;

    internal class Program
    {
        private static void Main(string[] args)
        {
            List<User> allUsers = ReadUsers("users.json");
            List<Post> allPosts = ReadPosts("posts.json");


            #region Demo

            // 1 - find all users having email ending with ".net".
            var users1 = from user in allUsers
                         where user.Email.EndsWith(".net")
                         select user;

            var users11 = allUsers.Where(user => user.Email.EndsWith(".net"));

            IEnumerable<string> userNames = from user in allUsers
                                            select user.Name;

            var userNames2 = allUsers.Select(user => user.Name);

            foreach (var value in userNames2)
            {
                Console.WriteLine(value);
            }

            IEnumerable<Company> allCompanies = from user in allUsers
                                                select user.Company;

            var users2 = from user in allUsers
                         orderby user.Email
                         select user;

            var netUser = allUsers.First(user => user.Email.Contains(".net"));
            Console.WriteLine(netUser.Username);

            #endregion

            // 2 - find all posts for users having email ending with ".net".
            var usersIdsWithDotNetMails = from user in allUsers
                                                       where user.Email.EndsWith(".net")
                                                       select user.Id;

            var posts = from post in allPosts
                                      where usersIdsWithDotNetMails.Contains(post.UserId)
                                      select post;

            foreach (var post in posts)
            {
                Console.WriteLine(post.Id + " " + "user: " + post.UserId);
            }

            // 3 - print number of posts for each user.
           


           
            // 4 - find all users that have lat and long negative.
            var us = from k in allUsers
                     where (k.Address.Geo.Lat < 0 && k.Address.Geo.Lng < 0)
                     select k;
            foreach (var item in us)
            {
                System.Console.WriteLine(item.Name + " " + item.Username);

            }

            Console.WriteLine("----------------------------------------");
            // 5 - find the post with longest body.
            var ad = allPosts.Max(s => s.Body.Length);
            foreach (var item in allPosts)
            {
                if (item.Body.Length == ad)
                {
                    Console.WriteLine($"Postnumber: {item.Id}");
                }
            }
           System.Console.WriteLine("======================================================");

            // 6 - print the name of the employee that have post with longest body.
            
            var userlong = from p in allPosts
                           where (p.Body.Length == allPosts.Max(s => s.Body.Length))
                           select p.UserId;
            var u = from p in allUsers
                    where userlong.Contains(p.Id)
                    select p.Name ;
            foreach (var item in u)
            {
                Console.WriteLine($"salut{item}");
            }
            Console.WriteLine("++++++++++++++++++++++++++++++++"); 


            // 7 - select all addresses in a new List<Address>. print the list.
            var list = new List<Address>();
            var a = from p in allUsers
                    select (p.Address);
            foreach (var item in a)
            {
                list.Add(item);
            }
            foreach (var address in list)
            {
                Console.WriteLine($"{address.Street}  {address.Suite}  {address.City} {address.Zipcode}");
            }
            Console.WriteLine("--------------------------------");
            // 8 - print the user with min lat
            var ads = allUsers.Min(p => p.Address.Geo.Lat);
            foreach (var item in allUsers)
            {
                if (item.Address.Geo.Lat==ads)
                {
                    Console.WriteLine($"{item.Name} {item.Username}");
                }
            }
            Console.WriteLine("-----------------------");
            
            // 9 - print the user with max long
            var userlong1 = allUsers.Max(s => s.Address.Geo.Lng);
            var userN = from p in allUsers
                        where (p.Address.Geo.Lng == userlong1)
                        select (p.Name+p.Username);
            foreach (var item in userN)
            {
                Console.WriteLine(item);
            }
            Console.WriteLine("---------------------------------");
            // 10 - create a new class: public class UserPosts { public User User {get; set}; public List<Post> Posts {get; set} }
            //    - create a new list: List<UserPosts>
            //    - insert in this list each user with his posts only
            var userpost = new List<UserPosts>();
            var qw = from p in allUsers
                     select p.Id;

            var we = from p in allPosts
                     where qw.Contains(p.UserId)
                     select p.UserId ;
            foreach (var item in we)
            {
                Console.WriteLine(item.);
            }


            // 11 - order users by zip code
            var userszip = from p in allUsers
                           orderby p.Address.Zipcode descending
                           select p;
            foreach (var item in userszip)
            {
                Console.WriteLine($"Zipcode: {item.Address.Zipcode}  Userul: {item.Name} {item.Username};");
            }

            // 12 - order users by number of posts
            var post1 = from p in allPosts
                       
                        orderby p.Id ascending
                        select p;
           // decit asa o varianta
            foreach (var user in allUsers)
            {
                Console.WriteLine();
                Console.WriteLine($"Utilizator: ID {user.Id} {user.Name} {user.Username}  are urm. idposturi:");
               
                foreach (var post in post1)
                {
                    if (post.UserId==user.Id)
                    {
                        Console.WriteLine($" Idpost {post.Id}  ");
                    }
                }
            }
            

            System.Console.Read();
        }
        class UserPosts 
        { 
            public User User { get; set; }
            public List<Post> Posts { get; set; }
            public void Adds(Post i)
            {
                Posts.Add(i);
            }
            
        }

        private static List<Post> ReadPosts(string file)
        {
            return ReadData.ReadFrom<Post>(file);
        }

        private static List<User> ReadUsers(string file)
        {
            return ReadData.ReadFrom<User>(file);
        }
    }
}
