namespace AhmadOmarAlAbass_200001037_Assignment_1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RecipeController controller = new RecipeController();
            while (true)
            {
                // عرض القائمة الرئيسية لإدارة الوصفات
                Console.WriteLine("\n--- Cooking Management ---");
                Console.WriteLine("1. Add New Recipe");
                Console.WriteLine("2. View All Recipes");
                Console.WriteLine("3. Edit Recipe");
                Console.WriteLine("4. Delete Recipe");
                Console.WriteLine("5. Exit");
                Console.Write("Enter your choice: ");

                string input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        controller.CreateRecipe(); // استدعاء دالة إضافة وصفة جديدة
                        break;
                    case "2":
                        controller.ShowRecipes(); // استدعاء دالة عرض جميع الوصفات
                        break;
                    case "3":
                        controller.EditRecipe(); // استدعاء دالة تعديل وصفة
                        break;
                    case "4":
                        controller.RemoveRecipe(); // استدعاء دالة حذف وصفة
                        break;
                    case "5":
                        return; // إنهاء البرنامج
                    default:
                        Console.WriteLine("Invalid choice, try again."); // التعامل مع إدخال غير صحيح
                        break;
                }
            }
        }
    }

    // تعريف تصنيفات الجمهور المستهدف للوصفات
    enum AudienceType
    {
        Personal,
        General
    }

    // نموذج بيانات للوصفة
    class CookingRecipe
    {
        public int RecipeId { get; set; } // معرف الوصفة
        public string Name { get; set; } // اسم الوصفة
        public List<string> Components { get; set; } // قائمة المكونات
        public string Guide { get; set; } // طريقة التحضير
        public string Cook { get; set; } // اسم الطاهي
        public AudienceType Audience { get; set; } // تصنيف الجمهور المستهدف
    }

    // صف لإدارة الوصفات وتخزينها في الذاكرة
    class RecipeController
    {
        private List<CookingRecipe> recipeList = new List<CookingRecipe>(); // قائمة تحتوي على جميع الوصفات
        private int recipeCounter = 1; // عداد لإعطاء معرف فريد لكل وصفة

        // دالة لإضافة وصفة جديدة
        public void CreateRecipe()
        {
            CookingRecipe recipe = new CookingRecipe();
            recipe.RecipeId = recipeCounter++;

            Console.Write("Enter recipe name: ");
            recipe.Name = Console.ReadLine();

            Console.Write("Enter chef name: ");
            recipe.Cook = Console.ReadLine();

            Console.Write("Enter preparation instructions: ");
            recipe.Guide = Console.ReadLine();

            Console.Write("Enter ingredients (separated by commas): ");
            recipe.Components = new List<string>(Console.ReadLine().Split(","));

            Console.Write("Select category (Personal / General): ");
            if (Enum.TryParse(Console.ReadLine(), out AudienceType type))
            {
                recipe.Audience = type;
            }
            else
            {
                Console.WriteLine("Invalid input, defaulting to General category.");
                recipe.Audience = AudienceType.General;
            }

            recipeList.Add(recipe);
            Console.WriteLine("Recipe added successfully!");
        }

        // دالة لعرض جميع الوصفات
        public void ShowRecipes()
        {
            if (recipeList.Count == 0)
            {
                Console.WriteLine("No recipes available.");
                return;
            }

            foreach (var recipe in recipeList)
            {
                Console.WriteLine($"\nRecipe ID: {recipe.RecipeId}" +
                    $"\nName: {recipe.Name}" +
                    $"\nChef: {recipe.Cook}" +
                    $"\nInstructions: {recipe.Guide}" +
                    $"\nIngredients: {string.Join(", ", recipe.Components)}" +
                    $"\nCategory: {recipe.Audience}\n");
            }
        }

        // دالة لتعديل وصفة موجودة
        public void EditRecipe()
        {
            Console.Write("Enter the recipe ID to edit: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                CookingRecipe recipe = recipeList.FirstOrDefault(r => r.RecipeId == id);
                if (recipe != null)
                {
                    Console.Write("Enter new name: ");
                    recipe.Name = Console.ReadLine();

                    Console.Write("Enter new chef name: ");
                    recipe.Cook = Console.ReadLine();

                    Console.Write("Enter new instructions: ");
                    recipe.Guide = Console.ReadLine();

                    Console.Write("Enter new ingredients (separated by commas): ");
                    recipe.Components = new List<string>(Console.ReadLine().Split(","));

                    Console.Write("Select new category (Personal / General): ");
                    if (Enum.TryParse(Console.ReadLine(), out AudienceType type))
                    {
                        recipe.Audience = type;
                    }
                    Console.WriteLine("Recipe updated successfully!");
                }
                else
                {
                    Console.WriteLine("Recipe not found.");
                }
            }
        }

        // دالة لحذف وصفة
        public void RemoveRecipe()
        {
            Console.Write("Enter the recipe ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                CookingRecipe recipe = recipeList.FirstOrDefault(r => r.RecipeId == id);
                if (recipe != null)
                {
                    Console.Write("Are you sure you want to delete? (yes/no): ");
                    string confirmation = Console.ReadLine();
                    if (confirmation.ToLower() == "yes")
                    {
                        recipeList.Remove(recipe);
                        Console.WriteLine("Recipe deleted successfully!");
                    }
                }
                else
                {
                    Console.WriteLine("Recipe not found.");
                }
            }
        }
    }
}
