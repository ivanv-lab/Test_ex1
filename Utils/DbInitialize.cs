using Test_ex.Models;

namespace Test_ex.Utils
{
    public class DbInitialize
    {
        public static async void Initialize(AppDbContext context)
        {
            await context.Database.EnsureCreatedAsync();

            var regions = new[]
            {
                    new Region{Number="33"},
                    new Region{Number="29"},
                    new Region{Number="24"},
                    new Region{Number="34"},
                    new Region{Number="35"},
                    new Region{Number="67"},
                    new Region{Number="72"},
                    new Region{Number="74"},
                    new Region{Number="18"},
                    new Region{Number="197"},
                    new Region{Number="199"},
                    new Region{Number="799"},
                    new Region{Number="759"},
                    new Region{Number="12"},
                    new Region{Number="05"}
            };

            var cabinets = new[]
            {
                    new Cabinet{Number="1"},
                    new Cabinet{Number="2"},
                    new Cabinet{Number="3"},
                    new Cabinet{Number="4"},
                    new Cabinet{Number="5А"},
                    new Cabinet{Number="5Б"},
                    new Cabinet{Number="7Г"},
                    new Cabinet{Number="8"},
                    new Cabinet{Number="110"},
                    new Cabinet{Number="11"},
                    new Cabinet{Number="111"},
                    new Cabinet{Number="104"},
                    new Cabinet{Number="347"},
                    new Cabinet{Number="10"},
                    new Cabinet{Number="11"}
            };

            var specs = new[] {
                new Specialization{Name="Хирург"},
                new Specialization{Name="ЛОР"},
                new Specialization{Name="Терапевт"},
                new Specialization{Name="Психотерапевт"},
                new Specialization{Name="Психолог"},
                new Specialization{Name="Аллерголог"},
                new Specialization{Name="Кардиолог"},
                new Specialization{Name="Гастроэнтеролог"},
                new Specialization{Name="Эндокринолог"},
                new Specialization{Name="Невролог"},
                new Specialization{Name="Онколог"},
                new Specialization{Name="Офтальмолог"},
                new Specialization{Name="Травматолог"},
                new Specialization{Name="Дерматолог"},
                new Specialization{Name="Педиатр"},
                new Specialization{Name="Анестезиолог"},
            };

            await context.Specializations.AddRangeAsync(specs);
            await context.Regions.AddRangeAsync(regions);
            await context.Cabinets.AddRangeAsync(cabinets);
            await context.SaveChangesAsync();

        }
    }
}
