using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace JavaPuzzler.MussInDenKopf
{
    public class Kampagne
    {
        public string Text { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Kampagne k = obj as Kampagne;
            if (k == null)
            {
                return false;
            }

            return EqualityComparer<string>.Default.Equals(k.Text, this.Text);
        }

        public override int GetHashCode()
        {
            if (this.Text == null) { return 0; }
            return this.Text.GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            ArrayList kopf = new ArrayList();
            kopf.Add("Bin ich schon drinn?");
            kopf.Add(new Kampagne() { Text = "Is den heid scho Weihnachtn?" });
            kopf.Add(11880);

            Auswertung(kopf);

            Console.Read();
        }

        private static void Auswertung(ArrayList kopf)
        {
            Console.WriteLine(kopf.Contains("Bin ich schon drinn?"));
            Console.WriteLine(kopf.Contains(new Kampagne() { Text = "Is den heid scho Weihnachtn?" }));
            Console.WriteLine(kopf.Contains(11880));
        }
    }
}
