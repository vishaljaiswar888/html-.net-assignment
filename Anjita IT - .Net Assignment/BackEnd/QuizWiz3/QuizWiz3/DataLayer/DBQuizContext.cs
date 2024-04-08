using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using QuizWiz3.Models;

namespace QuizWiz3.DataLayer
{
    public class DBQuizContext : DbContext
    {
        public DBQuizContext(DbContextOptions<DBQuizContext> options) : base(options)
        {

        }
        public DbSet<Submit> Submits { get; set; }
        public DbSet<Quiz> Quizs { get; set; }
    }
}
