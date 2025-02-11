using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Recipes.Data;
using Recipes.Data.Entities;
using Recipes.Data.Entities.Identity;
using System.Reflection.Metadata;

namespace Recipes.Seeders
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(
            ApplicationDbContext dbContext, 
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            await SeedUsersAsync(userManager, roleManager);
            await SeedCategoriesAsync(dbContext);
            await SeedRecipeNationaliesAsync(dbContext);
            await SeedRecipeTypesAsync(dbContext);
            await SeedRecipesAsync(dbContext);
            await RemoveDuplicateIngredientsAsync(dbContext);
        }

        private static async Task SeedUsersAsync(
            UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.Roles.Any())
            {
                IdentityRole admin = new IdentityRole(Constants.ADMINISTRATOR_ROLE);
                IdentityRole user = new IdentityRole(Constants.USER_ROLE);

                await roleManager.CreateAsync(admin);
                await roleManager.CreateAsync(user);
            }

            IList<ApplicationUser> administratorUsers = await userManager.GetUsersInRoleAsync(Constants.ADMINISTRATOR_ROLE);
            if (administratorUsers.Count() <= 0)
            {
                ApplicationUser applicationUser = new ApplicationUser()
                {
                    Email = "stefko@abv.bg",
                    UserName = "stefko@abv.bg",
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(applicationUser, "Pa$$w0rd");
                await userManager.AddToRoleAsync(applicationUser, Constants.ADMINISTRATOR_ROLE);
            }
        }

        private static async Task SeedCategoriesAsync(ApplicationDbContext dbContext)
        {
            if (await dbContext.Categories.AnyAsync())
            {
                return;
            }

            IEnumerable<Category> categories = new List<Category>()
            {
                new Category
                {
                    Name = "Месни"
                },
                new Category
                {
                    Name = "Млечни"
                },
                new Category
                {
                    Name = "Тестени"
                },
                new Category
                {
                    Name = "Зърнени и бобови"
                },
                new Category
                {
                    Name = "Сосове"
                },
                new Category
                {
                    Name = "Консервиране и туршии"
                },
                new Category
                {
                    Name = "Сладки"
                },
                new Category
                {
                    Name = "Солени"
                },
                new Category
                {
                    Name = "Напитки"
                },
                new Category
                {
                    Name = "Фитнес и здравословни"
                },
                new Category
                {
                    Name = "Вегетариански"
                },
                new Category
                {
                    Name = "Веган"
                },
                new Category
                {
                    Name = "Детски"
                },
                new Category
                {
                    Name = "Празнични и специални поводи"
                },
                new Category
                {
                    Name = "Улична храна"
                },
                new Category
                {
                    Name = "Гурме"
                },
                new Category
                {
                    Name = "Супи"
                },
                new Category
                {
                    Name = "Предястия и разядки"
                },
                new Category
                {
                    Name = "Барбекю и грил"
                },
                new Category
                {
                    Name = "Екзотични ястия"
                }
            };

            await dbContext.Categories.AddRangeAsync(categories);
            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedRecipeNationaliesAsync(ApplicationDbContext dbContext)
        {
            if (await dbContext.RecipeNationalities.AnyAsync())
            {
                return;
            }

            IEnumerable<RecipeNationality> recipeNationalities = new List<RecipeNationality>()
            {
                new RecipeNationality
                {
                    Name = "Българска"
                },
                new RecipeNationality
                {
                    Name = "Италианска"
                },
                new RecipeNationality
                {
                    Name = "Френска"
                },
                new RecipeNationality
                {
                    Name = "Гръцка"
                },
                new RecipeNationality
                {
                    Name = "Испанска"
                },
                new RecipeNationality
                {
                    Name = "Турска"
                },
                new RecipeNationality
                {
                    Name = "Мексиканска"
                },
                new RecipeNationality
                {
                    Name = "Японска"
                },
                new RecipeNationality
                {
                    Name = "Китайска"
                },
                new RecipeNationality
                {
                    Name = "Тайландска"
                },
                new RecipeNationality
                {
                    Name = "Индийска"
                },
                new RecipeNationality
                {
                    Name = "Мароканска"
                },
                new RecipeNationality
                {
                    Name = "Американска"
                },
                new RecipeNationality
                {
                    Name = "Руска"
                },
                new RecipeNationality
                {
                    Name = "Сръбска"
                },
                new RecipeNationality
                {
                    Name = "Виетнамска"
                },
                new RecipeNationality
                {
                    Name = "Бразилска"
                },
                new RecipeNationality
                {
                    Name = "Немска"
                },
                new RecipeNationality
                {
                    Name = "Арабска"
                },
                new RecipeNationality
                {
                    Name = "Корейска"
                }
            };

            await dbContext.RecipeNationalities.AddRangeAsync(recipeNationalities);
            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedRecipeTypesAsync(ApplicationDbContext dbContext)
        {
            if (await dbContext.RecipeTypes.AnyAsync())
            {
                return;
            }

            IEnumerable<RecipeType> recipeTypes = new List<RecipeType>()
            {
                new RecipeType
                {
                    Name = "Бързи рецепти (до 30 минути)"
                },
                new RecipeType
                {
                    Name = "Рецепти с 5 съставки или по-малко"
                },
                new RecipeType
                {
                    Name = "Еднотипни ястия"
                },
                new RecipeType
                {
                    Name = "Ястия на тиган"
                },
                new RecipeType
                {
                    Name = "Ястия на пара"
                },
                new RecipeType
                {
                    Name = "Ястия без готвене"
                },
                new RecipeType
                {
                    Name = "Ястия готвени на бавен огън"
                },
                new RecipeType
                {
                    Name = "Фюжън рецепти"
                },
                new RecipeType
                {
                    Name = "Безглутенови рецепти"
                },
                new RecipeType
                {
                    Name = "Нискокалорични рецепти"
                },
                new RecipeType
                {
                    Name = "Протеинови рецепти"
                },
                new RecipeType
                {
                    Name = "Рецепти за пикник"
                },
                new RecipeType
                {
                    Name = "Рецепти за мултикукър"
                },
                new RecipeType
                {
                    Name = "Рецепти за хлебопекарна"
                },
                new RecipeType
                {
                    Name = "Рецепти за фритюрник"
                },
                new RecipeType
                {
                    Name = "Ястия на фурна"
                },
                new RecipeType
                {
                    Name = "Рецепти за експериментиране"
                },
                new RecipeType
                {
                    Name = "Рецепти от 3 стъпки"
                },
                new RecipeType
                {
                    Name = "Рецепти за студенти"
                },
                new RecipeType
                {
                    Name = "Рецепти за романтична вечеря"
                }
            };

            await dbContext.RecipeTypes.AddRangeAsync(recipeTypes);
            await dbContext.SaveChangesAsync();
        }

        private static async Task SeedRecipesAsync(ApplicationDbContext dbContext)
        {
            if (await dbContext.Recipes.AnyAsync())
            {
                return;
            }

            //first recipe
            Category category = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "Тестени");
            RecipeType recipeType = await dbContext.RecipeTypes.FirstOrDefaultAsync(t => t.Name == "Ястия на тиган");
            RecipeNationality recipeNationality = await dbContext.RecipeNationalities.FirstOrDefaultAsync(n => n.Name == "Италианска");

            //second recipe
            Category category1 = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "Предястия и разядки");
            RecipeType recipeType1 = await dbContext.RecipeTypes.FirstOrDefaultAsync(t => t.Name == "Ястия на тиган");
            RecipeNationality recipeNationality1 = await dbContext.RecipeNationalities.FirstOrDefaultAsync(n => n.Name == "Арабска");

            //third recipe
            Category category2 = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "Сладки");
            RecipeType recipeType2 = await dbContext.RecipeTypes.FirstOrDefaultAsync(t => t.Name == "Бързи рецепти (до 30 минути)");
            RecipeNationality recipeNationality2 = await dbContext.RecipeNationalities.FirstOrDefaultAsync(n => n.Name == "Американска");

            //fourth recipe
            Category category3 = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "Екзотични ястия");
            RecipeType recipeType3 = await dbContext.RecipeTypes.FirstOrDefaultAsync(t => t.Name == "Рецепти за експериментиране");
            RecipeNationality recipeNationality3 = await dbContext.RecipeNationalities.FirstOrDefaultAsync(n => n.Name == "Гръцка");

            //fifth recipe
            Category category4 = await dbContext.Categories.FirstOrDefaultAsync(c => c.Name == "Месни");
            RecipeType recipeType4 = await dbContext.RecipeTypes.FirstOrDefaultAsync(t => t.Name == "Рецепти за романтична вечеря");
            RecipeNationality recipeNationality4 = await dbContext.RecipeNationalities.FirstOrDefaultAsync(n => n.Name == "Китайска");

            if (category == null || recipeType == null || recipeNationality == null)
            {
                return;
            }

            IEnumerable<Recipe> recipes = new List<Recipe>()
            {
                new Recipe
                {
                    Name = "Спагети с кайма",
                    Portions = 4,
                    Photo = "https://res.cloudinary.com/du73gcrdw/image/upload/v1739266209/pexels-photo-1279330_ap9ja3.jpg",
                    PreparationTime = 15,
                    CookingTime = 20,
                    Difficulty = 2,
                    CategoryId = category.Id,
                    RecipeTypeId = recipeType.Id,
                    RecipeNationalityId = recipeNationality.Id,
                    Description = "Сваряват се спагетите, изцеждат се и се оставят да изстинат.\r\n\r\n" +
                    "През това време се запържва лука и каймата прибавят се босилекът и риганът, оставя се да изстине.\r\n\r\n" +
                    "Към изстиналите спагети се прибавя киселото мляко, майонезата и царевицата разбъркват се и накрая се прибавя сместа от кайма и лук.",
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Спагети"
                            },
                            Quantity = "1 пакет"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Кисело мляко"
                            },
                            Quantity = "1 кофичка"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Майонеза"
                            },
                            Quantity = "250 гр."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Лук"
                            },
                            Quantity = "1 гл."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Кайма"
                            },
                            Quantity = "250 гр."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Царевица "
                            },
                            Quantity = "50 гр."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Босилек"
                            },
                            Quantity = "На вкус"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Риган"
                            },
                            Quantity = "На вкус"
                        }
                    }
                },
                new Recipe
                {
                    Name = "Шакшкука - яйца в доматен сос",
                    Portions = 1,
                    Photo = "https://res.cloudinary.com/du73gcrdw/image/upload/v1739269454/shakshuka_yaytsa_v_domaten_sos_2_rcnm9t.jpg",
                    PreparationTime = 10,
                    CookingTime = 20,
                    Difficulty = 4,
                    CategoryId = category1.Id,
                    RecipeTypeId = recipeType1.Id,
                    RecipeNationalityId = recipeNationality1.Id,
                    Description = "За тази рецепта с яйца на фурна нарязваме чушките на кубчета, лукът на полумесеци, чесъна на филийки и магданоза на ситно.\r\n\r\n" +
                    "Запържваме чушките за около 5 мин., като ги посоляваме (ако искате може да използвате и печени чушки), след това добавяме лукът и чесъна, и тях също ги запържваме за 2 мин. заедно с чушките.\r\n\r\n" +
                    "Сега добавяме доматения сос и подправките, и ги оставяме да покъкрят за 1-2 мин.\r\n\r\n" +
                    "Изсипваме зеленчуците с доматения сос в чугунен съд или тавичка, и чукваме яйцата отгоре. Така ги пускаме да се запичат за около 15 мин на 180 градуса и ястието шакшука - яйца в доматен сос е готово.\r\n\r\n" +
                    "Поръсете с пушен пипер, магданоз и малко зехтин.",
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Чушки"
                            },
                            Quantity = "1 бр. червена"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Чушки"
                            },
                            Quantity = "1 бр. зелена"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Чесън"
                            },
                            Quantity = "2 ск."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Лук"
                            },
                            Quantity = "1 гл."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Магданоз "
                            },
                            Quantity = "1/4 вр."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Яйца "
                            },
                            Quantity = "4 бр."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Доматен сос"
                            },
                            Quantity = "400 мл"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Сол"
                            },
                            Quantity = "На вкус"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Черен пипер"
                            },
                            Quantity = "На вкус"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Зехтин"
                            },
                            Quantity = "На вкус"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Чили"
                            },
                            Quantity = "Сухо"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Пушен червен пипер"
                            },
                            Quantity = "На вкус"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Кимион"
                            },
                            Quantity = "На вкус"
                        }
                    }
                },
                new Recipe
                {
                    Name = "Американски палачинки",
                    Portions = 6,
                    Photo = "https://res.cloudinary.com/du73gcrdw/image/upload/v1739271293/pexels-photo-376464_ppw5ld.jpg",
                    PreparationTime = 10,
                    CookingTime = 15,
                    Difficulty = 3,
                    CategoryId = category2.Id,
                    RecipeTypeId = recipeType2.Id,
                    RecipeNationalityId = recipeNationality2.Id,
                    Description = "Разбийте яйцата със захарта и добавете малко от брашното с бакпулвера, мазнината, захарта, солта и останалото брашно.\r\n\r\n" +
                    "Постепенно долейте и прясното мляко, като разбивате с тел. Бъркането не трябва да е дълго - щом продуктите се хомогенизират, спрете да бъркате.\r\n\r\n" +
                    "Загрейте тефлонов тиган, леко намазнен, и сипвайте по черпак от сместа. Не разстилайте палачинките.\r\n\r\n" +
                    "Щом станат на леки шупли отгоре, се обръщат. Гарнирайте палачинките със сладка плънка по избор.\r\n\r\n" +
                    "P.S. За да не стават на шупли, печете американските палачинки на по-слаб котлон!",
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Яйца"
                            },
                            Quantity = "2 бр."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Брашно"
                            },
                            Quantity = "2 ч.ч."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Мазнина"
                            },
                            Quantity = "2 с.л. олио или разтопено масло"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Прясно мляко"
                            },
                            Quantity = "250 - 260 мл"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Бакпулвер"
                            },
                            Quantity = "2 ч.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Захар"
                            },
                            Quantity = "2 с.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Сол"
                            },
                            Quantity = "1/4 ч.л."
                        }
                    }
                },
                new Recipe
                {
                    Name = "Лимонови тигрови скариди",
                    Portions = 4,
                    Photo = "https://res.cloudinary.com/du73gcrdw/image/upload/v1739272067/roasted-butterflied-tiger-prawns-in-garlic-butter-251335770_py6gnw.jpg",
                    PreparationTime = 10,
                    CookingTime = 15,
                    Difficulty = 5,
                    CategoryId = category3.Id,
                    RecipeTypeId = recipeType3.Id,
                    RecipeNationalityId = recipeNationality3.Id,
                    Description = "Почистете скаридите, като е желателно да им махнете главите. Загрейте леко зехтина с едро нарязания чесън в подходящ тиган и добавете тигровите скариди.\r\n\r\n" +
                    "Разбъркайте, сложете капак на съда и гответе, докато морските дарове си променят цвета, като през това време често разбърквате.\r\n\r\n" +
                    "Малко преди да ги свалите от котлона, долейте прясно изцедения лимонов сок, поръсете подправките и скълцания магданоз.",
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Скариди"
                            },
                            Quantity = "500 г тигрови"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Чесън"
                            },
                            Quantity = "6 скилидки"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Лимони"
                            },
                            Quantity = "2 бр."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Зехтин"
                            },
                            Quantity = "4 - 5 с.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Магданоз"
                            },
                            Quantity = "1/3 връзка"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Черен пипер"
                            },
                            Quantity = "Прясно смлян"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Сол"
                            },
                            Quantity = "На вкус"
                        }
                    }
                },
                new Recipe
                {
                    Name = "Пържено телешко с лук по китайски",
                    Portions = 3,
                    Photo = "https://res.cloudinary.com/du73gcrdw/image/upload/v1739273149/Mongolian-Beef-1_pnqfjv.webp",
                    PreparationTime = 10,
                    CookingTime = 20,
                    Difficulty = 8,
                    CategoryId = category4.Id,
                    RecipeTypeId = recipeType4.Id,
                    RecipeNationalityId = recipeNationality4.Id,
                    Description = "Нарежете месото на много тънки парчета тип слайс.\r\n\r\n" +
                    "Аз ползвах месо от телешка плешка, което и ви препоръчвам, бут и филе също може, но бутът е по-сух и жилав, трябва му повече време за готвене, а телешкото контрафиле пък е супер, но е и по-скъпо. Шарено месо от врат и гърди честно казано не съм срещала да ползват, затова доверете се на плешката.\r\n\r\n" +
                    "Сложете слайсовете месо в купа и сипете всички съставки, които съм описала за маринатата за телешко.\r\n\r\nРазбъркайте, така че всички съставки да покрият месото и оставете в хладилник за 2 часа да се маринова. Половин час преди готвене го извадете на стайна температура.\r\n\r\n" +
                    "Нарежете пресния лук на по широки парчета, кромида нарежете на полумесеци с дебелина около 2 см и след това с ръка разделете всяко, за да се получат отделни лентички.\r\n\r\nМоже да го нарежете и на едро, не е важно, просто не трябва да е на ситно, защото ще се сготви много бързо и има опасност да се смие и изчезне в тигана.\r\n\r\n" +
                    "Оставете лука настрани и направете соса.\r\n\r\n" +
                    "Смесете в купа всички съставки за соса, чесъна пресовайте, а нишестето размийте с водата и добавете и него. Разбъркайте всичко до хомогенна смес.\r\n\r\nСложете уок /дълбок/ тиган на котлона с 3 с.л олио и загрейте.\r\n\r\n" +
                    "Сложете лука и запържете за кратко, около 2 мин, колкото да пусне аромата си, и леко омекне.\r\n\r\n" +
                    "Добавете пресния лук, разбъркайте, гответе минута и извадете целия лук в чиния.\r\n\r\n" +
                    "Сипете още малко олио в тигана, ако е необходимо, аз сложих още 3 с.л. и загрейте.\r\n\r\n" +
                    "Изсипете месото в горещия тиган и запържете. В началото месото ще пусне малко течност, пържете и бъркайте периодично, докато остане само на мазнина. При мен отне около 15 минути.\r\n\r\n" +
                    "Когато телешкото видимо е променило цвета си, след тези 15 минути е започнало да се пържи, останало е на мазнина и е получило лек загар, тогава добавете сместа за соса.\r\n\r\n" +
                    "Разбъркайте, гответе 2 мин, сосът ще започне да се сгъстява от нишестето и ще се поеме частично от месото, тогава добавете запържения лук. Разбъркайте отново, докато съставките се обединят, гответе още минута и махнете от котлона.\r\n\r\n" +
                    "Сервирайте пържено телешко с лук по китайски в голяма чиния с пържен ориз по желание.",
                    Ingredients = new List<RecipeIngredient>()
                    {
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Телешко месо"
                            },
                            Quantity = "600 г"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name ="Вода"
                            },
                            Quantity = "20 мл."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Захар"
                            },
                            Quantity = "1 ч.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Черен пипер"
                            },
                            Quantity = "1/2 ч.л. млян"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Царевично нишесте"
                            },
                            Quantity = "1 ч.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Соев сос"
                            },
                            Quantity = "20 мл."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Сос от стриди"
                            },
                            Quantity = "1 ч.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Сода бикарбонат"
                            },
                            Quantity = "1/2 ч.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Олио"
                            },
                            Quantity = "1 ч.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Пресен лук"
                            },
                            Quantity = "1 връзка"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Кромид лук"
                            },
                            Quantity = "3 глави големи"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Соев сос"
                            },
                            Quantity = "50 мл"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Захар"
                            },
                            Quantity = "2 ч.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Сос от стриди"
                            },
                            Quantity = "2 ч.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Черен пипер"
                            },
                            Quantity = "1 щипка/ по вкус"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Сусамово олио"
                            },
                            Quantity = "1 с.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Царевично нишесте"
                            },
                            Quantity = "1 с.л. + вода 2 с.л."
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Чесън"
                            },
                            Quantity = "1 скилидка"
                        },
                        new RecipeIngredient
                        {
                            Ingredient = new Ingredient
                            {
                                Name = "Олио"
                            },
                            Quantity = "около 6 с.л."
                        }
                    }
                },
            };

            await dbContext.Recipes.AddRangeAsync(recipes);
            await dbContext.SaveChangesAsync();
        }

        private static async Task RemoveDuplicateIngredientsAsync(ApplicationDbContext dbContext)
        {
            // Retrieve all ingredients from the database
            var allIngredients = await dbContext.Ingredients.ToListAsync(); // Fetch data first

            var duplicates = allIngredients
                .GroupBy(i => i.Name)
                .Where(g => g.Count() > 1) // Only get duplicates
                .ToList();

            foreach (var duplicateGroup in duplicates)
            {
                var uniqueIngredient = duplicateGroup.First(); // Keep the first one
                var duplicateIngredients = duplicateGroup.Skip(1).ToList(); // Get the rest

                foreach (var duplicate in duplicateIngredients)
                {
                    // Update any RecipeIngredients that reference the duplicate ingredient
                    var recipeIngredients = dbContext.RecipeIngredients
                        .Where(ri => ri.IngredientId == duplicate.Id);

                    foreach (var recipeIngredient in recipeIngredients)
                    {
                        recipeIngredient.IngredientId = uniqueIngredient.Id;
                    }

                    // Remove the duplicate ingredient
                    dbContext.Ingredients.Remove(duplicate);
                }
            }

            await dbContext.SaveChangesAsync();
        }

    }
}
