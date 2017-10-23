using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using littlemm.teach.Model;

namespace littlemm.teach.Migrations
{
    [DbContext(typeof(ClassDb))]
    partial class ClassDbModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("littlemm.teach.Base.Class", b =>
                {
                    b.Property<string>("id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClassInfo");

                    b.Property<string>("ClassName");

                    b.Property<string>("StudentId");

                    b.Property<string>("Teacher");

                    b.Property<int>("section");

                    b.HasKey("id");

                    b.HasIndex("StudentId");

                    b.ToTable("Classes");
                });

            modelBuilder.Entity("littlemm.teach.Base.Student", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("littlemm.teach.Base.Class", b =>
                {
                    b.HasOne("littlemm.teach.Base.Student")
                        .WithMany("Classes")
                        .HasForeignKey("StudentId");
                });
        }
    }
}
