using digital.Backend.UnitsOfWork.Interfaces;
using digital.Shared.Entities;
using digital.Shared.Entities.Companies;
using digital.Shared.Entities.Countries;
using digital.Shared.Enums;
using Microsoft.EntityFrameworkCore;

namespace digital.Backend.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUsersUnitOfWork _usersUnitOfWork;

        public SeedDb(DataContext context, IUsersUnitOfWork usersUnitOfWork)
        {
            _context = context;
            _usersUnitOfWork = usersUnitOfWork;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            // await CheckCountriesFullAsync();
            await CheckCountriesAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1010", "Martin", "Valvo", "martin@yopmail.com", "3492 607557", "Calle los Jazmines", UserType.Admin);
            await CheckUserAsync("1011", "Rocio", "Armando", "rocio@yopmail.com", "3492 607558", "Calle los Jazmines", UserType.Admin);
            await CheckUserAsync("1012", "user", "user", "a@yopmail.com", "3492 607558", "Calle los Jazmines", UserType.User);
            //await CheckSectorsAsync();
            await CheckRubrosAndSectorsOfCompanyAsync();
            await CheckCategoriesOfCompanyAsync();
            await CheckCompaniesAsync();

        }

        private async Task CheckRubrosAndSectorsOfCompanyAsync()
        {
            if (!_context.Rubros.Any())
            {
                var rubros = new List<Rubro>
                {
                    new Rubro{ Name = "Agropecuario"},
                    new Rubro{ Name = "Industria y Mineria"},
                    new Rubro{ Name = "Servicios"},
                    new Rubro{ Name = "Construccion"},
                    new Rubro{ Name = "Comercio"}
                };
                _context.Rubros.AddRange(rubros);
                await _context.SaveChangesAsync();
            }
            if (!_context.Sectors.Any())
            {
                var sectors = new List<Sector>
                {
                    new Sector {Name = "Agricultura, Ganaderia, Silvicultura y Pesca", RubroId = 1 },
                    new Sector {Name = "Explotacion de minas y Canteras", RubroId = 2 },
                    new Sector {Name = "Transporte y Almacenamiento", RubroId = 2 },
                    new Sector {Name = "Informacion y Comunicacion", RubroId = 2 },
                    new Sector {Name = "Electricidad, Gas, Vapor y Aire acondicionado", RubroId = 3 },
                    new Sector {Name = "Suministros de Agua, Cloacas, Gestio de residuos y Recuperacion de Materiales", RubroId = 3 },
                    new Sector {Name = "Servicio de Transporte y Almacenamiento(No industria y Mineria)", RubroId = 3 },
                    new Sector {Name = "Servicio de Alojamiento y Servicio de Comidas", RubroId = 3 },
                    new Sector {Name = "Intermediacion Financiera y servicios de Seguros", RubroId = 3 },
                    new Sector {Name = "Servicios Inmobiliarios", RubroId = 3 },
                    new Sector {Name = "Actividades Profesionales, Cientificas y Tecnicas", RubroId = 3 },
                    new Sector {Name = "Actividades adminsitrativas y de Servicios de apoyo", RubroId = 3 },
                    new Sector {Name = "Enseñanza", RubroId = 3 },
                    new Sector {Name = "Salud humana y servicios Sociales", RubroId = 3 },
                    new Sector {Name = "Servicios Artisticos, Culturales, Deportivos y de Esparcimiento", RubroId = 3 },
                    new Sector {Name = "Servicios de Asociaciones y de Servicios Personales", RubroId = 3 },
                    new Sector {Name = "Construccion", RubroId = 4 },
                    new Sector {Name = "Comercio al por mayor y al por menor, Reparacion de Vehiculos automotores y Motocicletas", RubroId = 5 },

                };
                _context.Sectors.AddRange(sectors);
                await _context.SaveChangesAsync();
            }

        }


        private async Task CheckCategoriesOfCompanyAsync()
        {
            if (!_context.Categories.Any())
            {

                var country = await _context.Countries.FirstOrDefaultAsync(x => x.Name == "Argentina");
                country ??= await _context.Countries.FirstOrDefaultAsync();

                var sector = await _context.Sectors.FirstOrDefaultAsync(x => x.Name == "Enseñanza");
                sector ??= await _context.Sectors.FirstOrDefaultAsync();

                var categoriesOfCompany = new List<Category> 
                { 
                    new Category 
                    {
                        Name = "Micro",
                        //Country = country!,
                        CountryId = country!.Id,
                        RevenueLimit = 500000,
                        SectorId = sector!.Id,
                        
                    },
                    new Category
                    {
                        Name = "Pequeña",
                        //Country = country!,
                        CountryId = country!.Id,
                        SectorId = sector!.Id,
                        RevenueLimit = 10000000,

                    },
                    new Category
                    {
                        Name = "Mediana Tramo 1",
                        //Country = country!,
                        CountryId = country!.Id,
                        SectorId = sector!.Id,
                        RevenueLimit = 10000000,

                    },
                    new Category
                    {
                        Name = "Mediana Tramo 2",
                        //Country = country!,
                        CountryId = country!.Id,
                        RevenueLimit = 10000000,
                        SectorId = sector!.Id,

                    }
                };
                

                _context.Categories.AddRange(categoriesOfCompany);
                await _context.SaveChangesAsync();
            }
            
        }

        private async Task CheckCompaniesAsync()
        {
            if (!_context.Companies.Any())
            {
                //var sector = await _context.Sectors.FirstOrDefaultAsync(s => s.Description == "Technology");
                //sector ??= await _context.Sectors.FirstOrDefaultAsync(); // En caso de que no se encuentre el sector, toma el primero disponible

                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Rafaela");
                city ??= await _context.Cities.FirstOrDefaultAsync();

                var sector = await _context.Sectors.FirstOrDefaultAsync(x => x.Name == "Enseñanza");
                sector ??= await _context.Sectors.FirstOrDefaultAsync();

                var category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == "Micro");
                category ??= await _context.Categories.FirstOrDefaultAsync();

                var companies = new List<Company>
                {
                    new Company
                    {
                        Cuit = "123456789",
                        Name = "Tech Innovators",
                        CityId = city!.Id,
                        Email = "info@techinnovators.com",
                        WebPage = "www.techinnovators.com",
                        //Sector = sector,
                        SectorId = sector!.Id,
                        LegalForm = LegalFormsEnum.SRL,
                        Size = SizeCompanyEnum.DiezADiecinueve,
                        OwnFacilities = true,
                        PorcAdministracion = 10,
                        //Category = category,
                        CategoryId = category!.Id,
                        PorcComercializacion = 15,
                        PorcProduccion = 60,
                        PorcRRHH = 5,
                        PorcLogistica = 5,
                        PorcMantenimiento = 5,
                        PorcProductoDestinadoAMercadoLocal = 80,
                        PorcProductoDestinadoAMercadoExterior = 20,
                        Terciariza = false,
                        Observaciones = "Ninguna",
                        DateInsert = DateTime.UtcNow,
                        //DateDelete se quedan en null
                    },
                    new Company
                    {
                        Cuit = "987654321",
                        Name = "Tech Innovators 2",
                        CityId = city.Id,
                        Email = "info@techinnovators2.com",
                        WebPage = "www.techinnovators2.com",
                        LegalForm = LegalFormsEnum.SA,
                        //Sector = sector,
                        SectorId = sector!.Id,
                        Size = SizeCompanyEnum.DiezADiecinueve,
                        //Category = category,
                        CategoryId = category.Id,
                        OwnFacilities = true,
                        PorcAdministracion = 10,
                        PorcComercializacion = 15,
                        PorcProduccion = 60,
                        PorcRRHH = 5,
                        PorcLogistica = 5,
                        PorcMantenimiento = 5,
                        PorcProductoDestinadoAMercadoLocal = 80,
                        PorcProductoDestinadoAMercadoExterior = 20,
                        Terciariza = false,
                        Observaciones = "Ninguna",
                        DateInsert = DateTime.UtcNow,


                    }
                };

                _context.Companies.AddRange(companies);
                await _context.SaveChangesAsync();
            }
        }

        private async Task CheckCountriesFullAsync()
        {
            if (!_context.Countries.Any())
            {
                var countriesStatesCitiesSQLScript = File.ReadAllText("Data\\CountriesStatesCities.sql");
                await _context.Database.ExecuteSqlRawAsync(countriesStatesCitiesSQLScript);
            }

        }

        private async Task CheckRolesAsync()
        {
            // TODO: Aca agregas los roles de usuarios que quieras

            await _usersUnitOfWork.CheckRoleAsync(UserType.Admin.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.Intermediate.ToString());
            await _usersUnitOfWork.CheckRoleAsync(UserType.User.ToString());
        }


        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _usersUnitOfWork.GetUserAsync(email);
            if (user == null)
            {

                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Name == "Rafaela");
                city ??= await _context.Cities.FirstOrDefaultAsync();

                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Document = document,
                    //City = city,
                    UserType = userType,
                };

                await _usersUnitOfWork.AddUserAsync(user, "123456");
                await _usersUnitOfWork.AddUserToRoleAsync(user, userType.ToString());

                var token = await _usersUnitOfWork.GenerateEmailConfirmationTokenAsync(user);
                await _usersUnitOfWork.ConfirmEmailAsync(user, token);

            }

            return user;
        }



        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Antioquia",
                            Cities = new List<City>() {
                                new City() { Name = "Medellín" },
                                new City() { Name = "Itagüí" },
                                new City() { Name = "Envigado" },
                                new City() { Name = "Bello" },
                                new City() { Name = "Rionegro" },
                            }
                        },
                        new State()
                        {
                            Name = "Bogotá",
                            Cities = new List<City>() {
                                new City() { Name = "Usaquen" },
                                new City() { Name = "Champinero" },
                                new City() { Name = "Santa fe" },
                                new City() { Name = "Useme" },
                                new City() { Name = "Bosa" },
                            }
                        },
                    }

                });

                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Florida",
                            Cities = new List<City>() {
                                new City() { Name = "Orlando" },
                                new City() { Name = "Miami" },
                                new City() { Name = "Tampa" },
                                new City() { Name = "Fort Lauderdale" },
                                new City() { Name = "Key West" },
                            }
                        },
                        new State()
                        {
                            Name = "Texas",
                            Cities = new List<City>() {
                                new City() { Name = "Houston" },
                                new City() { Name = "San Antonio" },
                                new City() { Name = "Dallas" },
                                new City() { Name = "Austin" },
                                new City() { Name = "El Paso" },
                            }
                        },
                    }
                });
                _context.Countries.Add(new Country
                {
                    Name = "Argentina",
                    States = new List<State>()
                    {
                        new State()
                        {
                            Name = "Santa Fe",
                            Cities = new List<City>() {
                                new City() { Name = "Rafaela" },
                                new City() { Name = "Esperanza" },
                            }
                        }
                    }
                });
            }

            await _context.SaveChangesAsync();
        }

    }
}
