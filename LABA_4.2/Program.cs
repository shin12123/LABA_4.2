namespace LABA_4_2
//Страхування. Визначити ієрархію страхових зобов'язань. Зібрати із зобов'язань дериватив. Підрахувати вартість. 
//Провести сортування зобов'язань в деривативів на основі зменшення ступеня ризику. 
//Знайти зобов'язання в деривативів, відповідне заданому діапазону параметрів.

{

    class Insurance
    {
      
        public string Type { get; set; } 
        public string Name { get; set; } 
        public short Price { get; set; } 
        public short RiskLevel { get; set; } 

      
        public Insurance(string type, string name, short price, short riskLevel)
        {
            Type = type; 
            Name = name; 
            Price = price; 
            RiskLevel = riskLevel; 
        }

        
        public override string ToString()
        {
          
            return $"{Type}: {Name}, Цена: {Price}, Уровень риска: {RiskLevel}";
        }
    }


    class Derivative 
    {
       
        public List<Insurance> Insurances { get; set; } = new List<Insurance>();

      
        public void AddInsurance(Insurance insurance) => Insurances.Add(insurance);

     
        public decimal CalculateTotalPrice() => Insurances.Sum(insurance => insurance.Price);

        
        public void SortByRisk() => Insurances.Sort((a, b) => b.RiskLevel.CompareTo(a.RiskLevel));

        
        public IEnumerable<Insurance> FindByPriceRange(decimal minPrice, decimal maxPrice) =>
            Insurances.Where(insurance => insurance.Price >= minPrice && insurance.Price <= maxPrice);

       
        public override string ToString() => string.Join("\n", Insurances);
    }

   
    class Program
    {
        
        static void Main()
        {
            
            var derivative = new Derivative();

            
            var insuranceData = new[]
            {
               
                "Life,Жизнь Премиум,2000,5",
                "Life,Жизнь Стандарт,1500,2",
                "Health,Здоровье Плюс,950,3",
                "Health,Здоровье Базовый,500,4",
                "CAR,Авто Полный,800,4"
            };

            
            foreach (var line in insuranceData)
            {
               
                var parts = line.Split(',');
               
                derivative.AddInsurance(new Insurance(parts[0], parts[1], short.Parse(parts[2]), short.Parse(parts[3])));//
            }

                using (var writer = new StreamWriter(@"output.txt"))
            {
            
                writer.WriteLine("Страховки:");
                writer.WriteLine(derivative.ToString());

              
                writer.WriteLine($"\nОбщая стоимость: {derivative.CalculateTotalPrice()}");

                
                derivative.SortByRisk();
                writer.WriteLine("\nПосле сортировки по уровню риска:");
                writer.WriteLine(derivative.ToString());

                
                writer.WriteLine("\nСтраховки с ценой от 500 до 1500:");
                foreach (var insurance in derivative.FindByPriceRange(500, 1500))
                {
                    writer.WriteLine(insurance.ToString());
                }
            }
            Console.WriteLine("Вывод выполнен в файл 'output.txt'.");
        }
    }
}
