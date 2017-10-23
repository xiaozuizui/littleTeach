using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using littlemm.teach.Model;
namespace littlemm.teach.Base
{
    public class StToInfo
    {
        private const string nbsp = "nbsp";
        private const string pyfa = "培养方案</td>";
        public string dataTxt { get; set; }
        public char[] dataTxtChar { get; set; }

        public Student stu;
        
        public Class c;
        public StToInfo(string st)
        {
            c = new Class();
            stu = new Student();
            stu.Id = "201585081";
            stu.Name = "ZZ";

            stu.Classes = new List<Class>();
            using (var db = new ClassDb())
            {
                db.Students.Add(stu);
                db.SaveChanges();
            }
            dataTxt = st;
            dataTxtChar = dataTxt.ToCharArray();
        }

        public void SaveTodb()
        {
            int Index = 0;

            while (Index != dataTxt.Length)
            {
                Index = ReadInfo(Index);
            }

        }
        public int ReadInfo(int k)
        {
            int i, j;
            i = dataTxt.IndexOf(pyfa, k);
            if (i == -1) return -1;
                i = dataTxt.IndexOf(nbsp, i) + 5;
            j = dataTxt.IndexOf("<", i);
            c.id = new String(dataTxtChar, i, j - i);
            i = dataTxt.IndexOf(nbsp, i) + 5;
            j = dataTxt.IndexOf("<", i);
            c.ClassName = new String(dataTxtChar, i, j - i);
            i = j;
            for (int z = 0; z < 5; z++)
                i = dataTxt.IndexOf(";", i + 1);
            j = dataTxt.IndexOf("<", i);
            i = i + 1;
            c.Teacher = new String(dataTxtChar, i, j - i);
            i = dataTxt.IndexOf(nbsp, j);
            i = dataTxt.IndexOf(nbsp, i + 1);
            j = dataTxt.IndexOf(pyfa, i) - 20;
            if (j < 0)
                j = dataTxt.Length;

            int section = 0;
            string Info = null;
            while (i < j)
            {
                i = dataTxt.IndexOf(nbsp, i + 1);
                if (i == -1) break;
                if (i < j)
                {

                    if ((dataTxt.IndexOf("<", i) - i - 6) == -1) break;

                    Info += new String(dataTxtChar, i + 6, dataTxt.IndexOf("<", i) - i - 6);
                    Info += '#';
                    i = dataTxt.IndexOf(nbsp, i + 1);
                    Info += new String(dataTxtChar, i + 6, dataTxt.IndexOf("<", i) - i - 6);
                    Info += '#';

                    i = dataTxt.IndexOf(nbsp, i + 1);
                    Info += new String(dataTxtChar, i + 5, dataTxt.IndexOf("<", i) - i - 5);
                    Info += '#';

                    i = dataTxt.IndexOf(nbsp, i + 1);
                    Info += new String(dataTxtChar, i + 6, dataTxt.IndexOf("<", i) - i - 6);
                    Info += '#';

                    i = dataTxt.IndexOf(nbsp, i + 1);
                    Info += new String(dataTxtChar, i + 6, dataTxt.IndexOf("<", i) - i - 6);
                    Info += '#';

                    i = dataTxt.IndexOf(nbsp, i + 1);
                    Info += new String(dataTxtChar, i + 6, dataTxt.IndexOf("<", i) - i - 6);
                    Info += '#';

                    i = dataTxt.IndexOf(nbsp, i + 1);
                    Info += new String(dataTxtChar, i + 6, dataTxt.IndexOf("<", i) - i - 6);
                    Info += '#';

                    section++;
                }
                else { break; }
            }
            c.section = section;
            c.ClassInfo = Info;
            using (var db = new ClassDb())
            {
                var stu = db.Students.First();
                stu.Classes = new List<Class>();
                stu.Classes.Add(c);
                db.SaveChanges();
            }
                
            return j;
        }

    }
}
