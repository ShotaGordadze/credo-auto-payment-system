using Credo.Infrastructure.Entities;
using Credo.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Credo.Infrastructure;

public static class DbSeeder
{
    public static async Task SeedAsync(CredoDbContext dbContext)
    {
        if (!await dbContext.ProviderCategories.AnyAsync())
        {
            var electricity = new ProviderCategory { Name = "Electricity" };
            var water = new ProviderCategory { Name = "Water" };
            var gas = new ProviderCategory { Name = "Gas" };

            dbContext.ProviderCategories.AddRange(electricity, water, gas);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Providers.AnyAsync())
        {
            var categories = await dbContext.ProviderCategories
                .AsNoTracking()
                .ToDictionaryAsync(x => x.Name, x => x.Id);
            var providers = new List<Provider>
            {
                new Provider
                {
                    providerName = "Telmiko",
                    ProviderAccount = "5391196978250662",
                    ProviderCategoryId = categories["Electricity"]
                },
                new Provider
                {
                    providerName = "Georgian provider",
                    ProviderAccount = "5487932657020033",
                    ProviderCategoryId = categories["Electricity"]
                },
                new Provider
                {
                    providerName = "Georgian Water End Power",
                    ProviderAccount = "5519727551117469",
                    ProviderCategoryId = categories["Water"]
                },
                new Provider
                {
                    providerName = "Batumi Water",
                    ProviderAccount = "5163752829567906",
                    ProviderCategoryId = categories["Water"]
                },
                new Provider
                {
                    providerName = "Tbilisi energy",
                    ProviderAccount = "5589461051529230",
                    ProviderCategoryId = categories["Gas"]
                },
                new Provider
                {
                    providerName = "Socar Gas",
                    ProviderAccount = "5500597792249715",
                    ProviderCategoryId = categories["Gas"]
                }
            };

            dbContext.Providers.AddRange(providers);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Subscribers.AnyAsync())
        {
            var providerIds = await dbContext.Providers
                .AsNoTracking()
                .ToDictionaryAsync(x => x.providerName, x => x.Id);

            var random = new Random();

            var subscribers = new List<Subscriber>
            {
                new Subscriber
                {
                    Name = "Giorgi",
                    LastName = "Beridze",
                    SubscriberNumber = "0102345678",
                    ProviderId = providerIds["Telmiko"],
                    Debt = random.Next(-1000, 0),
                },
                new Subscriber
                {
                    Name = "Nika",
                    LastName = "Kapanadze",
                    SubscriberNumber = "0108765432",
                    ProviderId = providerIds["Telmiko"],
                    Debt = random.Next(-1000, 0),
                },

                new Subscriber
                {
                    Name = "Luka",
                    LastName = "Gelashvili",
                    SubscriberNumber = "599112233",
                    ProviderId = providerIds["Georgian provider"],
                    Debt = random.Next(-1000, 0),
                },
                new Subscriber
                {
                    Name = "Sandro",
                    LastName = "Tsintsadze",
                    SubscriberNumber = "599445566",
                    ProviderId = providerIds["Georgian provider"],
                    Debt = random.Next(-1000, 0),
                },

                new Subscriber
                {
                    Name = "Mariam",
                    LastName = "Chkhaidze",
                    SubscriberNumber = "0322987654",
                    ProviderId = providerIds["Georgian Water End Power"],
                    Debt = random.Next(-1000, 1000),
                },
                new Subscriber
                {
                    Name = "Tamar",
                    LastName = "Gogoladze",
                    SubscriberNumber = "0322765432",
                    ProviderId = providerIds["Batumi Water"],
                    Debt = random.Next(-1000, 0),
                },

                new Subscriber
                {
                    Name = "Irakli",
                    LastName = "Khutsishvili",
                    SubscriberNumber = "514123456",
                    ProviderId = providerIds["Tbilisi energy"],
                    Debt = random.Next(-1000, 0),
                },
                new Subscriber
                {
                    Name = "Dato",
                    LastName = "Abashidze",
                    SubscriberNumber = "514654321",
                    ProviderId = providerIds["Tbilisi energy"],
                    Debt = random.Next(-1000, 0),
                },

                new Subscriber
                {
                    Name = "Ana",
                    LastName = "Maisuradze",
                    SubscriberNumber = "577889900",
                    ProviderId = providerIds["Socar Gas"],
                    Debt = random.Next(-1000, 0),
                },
                new Subscriber
                {
                    Name = "Salome",
                    LastName = "Lomidze",
                    SubscriberNumber = "577001122",
                    ProviderId = providerIds["Socar Gas"],
                    Debt = random.Next(-1000, 0),
                }
            };

            dbContext.Subscribers.AddRange(subscribers);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.Accounts.AnyAsync())
        {
            var subscribers = await dbContext.Subscribers
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();

            var existingSubscriberIds = await dbContext.Accounts
                .IgnoreQueryFilters()
                .Select(x => x.SubscriberId)
                .ToListAsync();
            var newAccounts = new List<Account>();

            foreach (var subscriber in subscribers)
            {
                if (existingSubscriberIds.Contains(subscriber.Id))
                    continue;

                newAccounts.Add(new Account
                {
                    SubscriberId = subscriber.Id,
                    AccountNumber = $"GE00TB{subscriber.Id:D14}",
                    Currency = Currency.GEL,
                    Balance = Random.Shared.Next(200, 5000),
                    AccountType = AccountType.Visa
                });
            }

            dbContext.Accounts.AddRange(newAccounts);
            await dbContext.SaveChangesAsync();
        }

        if (!await dbContext.AutoPaymentAccounts.AnyAsync())
        {
            var accounts = await dbContext.Accounts
                .IgnoreQueryFilters()
                .AsNoTracking()
                .ToListAsync();

            var existingAutoPayments = await dbContext.AutoPaymentAccounts
                .IgnoreQueryFilters()
                .Select(x => x.AccountId)
                .ToListAsync();

            var autoPayments = new List<AutoPaymentAccount>();

            foreach (var account in accounts)
            {
                if (existingAutoPayments.Contains(account.Id))
                    continue;

                autoPayments.Add(new AutoPaymentAccount
                {
                    AccountId = account.Id,
                    TargetAccountNumber = account.AccountNumber,
                    StartDate = DateTime.UtcNow,
                    EndDate = DateTime.UtcNow.AddYears(1),
                    Amount = Random.Shared.Next(20, 300),
                    FrequencyInDays = Random.Shared.Next(1, 10)
                });
            }

            dbContext.AutoPaymentAccounts.AddRange(autoPayments);
            await dbContext.SaveChangesAsync();
        }
    }
}