using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JavaPuzzler.MussInDenKopf2
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<Kampagne, bool> kopf = new Dictionary<Kampagne, bool>();
            kopf.Add(new Kampagne() { Text = "Is den heid scho Weihnachtn?" }, true);

            Auswertung(kopf);

            Console.Read();
        }

        private static void Auswertung(Dictionary<Kampagne, bool> kopf)
        {
            Console.WriteLine(kopf[new Kampagne() { Text = "Is den heid scho Weihnachtn?" }]);
        }
    }
}
