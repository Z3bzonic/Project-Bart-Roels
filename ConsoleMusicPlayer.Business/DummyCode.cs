namespace ConsoleMusicPlayer.Business
{
    public class DummyCode
    {
        private string? _kleur;
        public string? Kleur
        {
            get
            {
                return _kleur;
                //if (DateTime.Now.Day % 2 == 0)
                //{
                //    //EVEN
                //    return "blauw";
                //}
                //else
                //    return "geel";
            }
            set
            {
                _kleur = value;
            }
        }

        public void Dummy()
        {
            //var cached = _statesDTO.GetSomethingHeavyFromDatabase();


            //using(var t = new TransactionScope())
            //{
            //    //1) save bedrijf

            //    //2) save de 5 werknemers

            //    //2b) : stel dat exception gebeurt bij opslaan van werknemer 4

            //    t.Complete();
            //}

            //using (var ex = new BusinessException())
            //{

            //}

            //using (var ms = new MemoryStream())
            //{

            //}
        }



        public void ReferenceAndValueTypes()
        {
            #region SAMPLE 1
            var i = 1;
            ChangeNumber(i);
            //i is 1 of 2 ?
            //==> i blijft 1
            #endregion SAMPLE 1

            #region SAMPLE 2
            var dog1 = new Dog() { Name = "Fifi" };
            ChangeDogName1(dog1, "FroeFroe");
            //dog1.Name wordt "FroeFroe"
            #endregion SAMPLE 2

            #region SAMPLE 3
            var dog2 = new Dog() { Name = "Fifi" };
            ChangeDogName2(dog2, "FroeFroe");
            //dog2.Name blijft "Fifi" ! omdat 
            #endregion SAMPLE 3

            #region SAMPLE 4
            var j = 1;
            ChangeNumberAsRef(ref j);   //NIET ECHT, maar soort van "BOXING naar een object" (zie hieronder)
            //j is 1 of 2 ?
            //==> i wordt 2, omdat enkel een pointer naar het geheugenadres van de int j wordt doorgegeven. m.a.w. de methode wijzigt j rechtstreeks
            var o = (object)j;      //BOXING
            #endregion SAMPLE 4
        }

        private void ChangeNumber(int i) => i++;

        private void ChangeDogName1(Dog dog, string name) => dog.Name = name;

        private void ChangeDogName2(Dog dog, string name) => dog = new Dog { Name = name };

        private void ChangeNumberAsRef(ref int j) => j++;       //NORMAAL NIET GEBRUIKEN !

        /// <summary>
        /// Het resultaat van deze methode zal een exception zijn, dus komt nooit aan de return, maar er zullen WEL 2 messages gereturned worden
        /// </summary>
        /// <param name="messages"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        private bool DoSomethingWithOutParam(out List<string> messages)
        {
            //initialisatie is VERPLICHT
            messages = new List<string>();

            //do vanalle random operaties, bv 1) lees input
            var x = Console.ReadLine();
            messages.Add("X coorrect ingelezen");
            
            var y = Console.ReadLine();
            messages.Add("Y coorrect ingelezen");

            var z = Console.ReadLine();
            throw new InvalidOperationException("SOME ERROR");
            messages.Add("Y coorrect ingelezen");

            return true;    //processing succeeded
        }

        private Tuple<int, string> SampleReturnMetTuple() => new Tuple<int, string>(1, "test");
    }

    public class Dog
    {
        public string? Name { get; set; }

        public virtual string MakeNoise()
        {
            return "Woof";
        }
    }

    public class Basil : Dog
    {
        public override string MakeNoise()
        {
            return "WOOF WOOF WOOF";
        }

        //public new string MakeNoise()
        //{
        //    return "WOOF WOOF WOOF";
        //}
    }

    /// <summary>
    /// de 4 pijlers van object georiënteerd programmeren => POLYMORFISME (VEELVORMIGHEID ?)
    /// </summary>
    public class OOTester
    {
        public void Run()
        {
            var dog = new Dog();
            dog.MakeNoise();        //Woof

            var basil = new Basil();
            basil.MakeNoise();      //WOOF WOOF WOOF

            //SPECIAL
            Dog basilAsDog = (Dog)basil;
            basilAsDog.MakeNoise();     // !!! WOOF WOOF WOOF (omdat er public override staat)

            //indien er "public new" gebruikt wordt
            basilAsDog.MakeNoise();     // !!! Woof (omdat er public new staat)
        }
    }
}