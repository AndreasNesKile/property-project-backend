using backend.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Data
{
    public class Seed
    {
        public static void SeedData(PropertyDbContext context)
        {
            SeedPropertyStatus(context);
            SeedPropertyType(context);
            SeedProperty(context);
            SeedPropertyImage(context);
            SeedRenovation(context);
            SeedValuation(context);
            SeedOwnerType(context);
            SeedOwner(context);
            SeedOwnershipLog(context);
            SeedAccountType(context);
            SeedAccount(context);
        }

        public static void SeedPropertyStatus(PropertyDbContext context)
        {
            if (!context.PropertyStatuses.Any())
            {
                var statusData = File.ReadAllText("./MockData/PropertyStatusSeedData.json");
                var statusList = JsonConvert.DeserializeObject<List<PropertyStatus>>(statusData);
                foreach (var status in statusList)
                {
                    context.PropertyStatuses.Add(status);
                }
                context.SaveChanges();
            }
        }

        public static void SeedPropertyType(PropertyDbContext context)
        {
            if (!context.PropertyTypes.Any())
            {
                var typeData = File.ReadAllText("./MockData/PropertyTypeSeedData.json");
                var types = JsonConvert.DeserializeObject<List<PropertyType>>(typeData);
                foreach (var type in types)
                {
                    context.PropertyTypes.Add(type);
                }
                context.SaveChanges();
            }
        }

        public static void SeedProperty(PropertyDbContext context)
        {
            if (!context.Properties.Any())
            {
                var propertyData = File.ReadAllText("./MockData/PropertySeedData.json");
                var properties = JsonConvert.DeserializeObject<List<Property>>(propertyData);
                foreach (var property in properties)
                {
                    context.Properties.Add(property);
                }
                context.SaveChanges();
            }
        }

        public static void SeedPropertyImage(PropertyDbContext context)
        {
            if (!context.PropertyImages.Any())
            {
                var propertyImageData = File.ReadAllText("./MockData/PropertyImageSeedData.json");
                var propertyImages = JsonConvert.DeserializeObject<List<PropertyImage>>(propertyImageData);
                foreach (var propertyImage in propertyImages)
                {
                    context.PropertyImages.Add(propertyImage);
                }
                context.SaveChanges();
            }
        }

        public static void SeedRenovation(PropertyDbContext context)
        {
            if (!context.Renovations.Any())
            {
                var renovationData = File.ReadAllText("./MockData/RenovationSeedData.json");
                var renovations = JsonConvert.DeserializeObject<List<Renovation>>(renovationData);
                foreach (var renovation in renovations)
                {
                    context.Renovations.Add(renovation);
                }
                context.SaveChanges();
            }
        }

        public static void SeedValuation(PropertyDbContext context)
        {
            if (!context.Valuations.Any())
            {
                var valuationData = File.ReadAllText("./MockData/ValuationSeedData.json");
                var valuations = JsonConvert.DeserializeObject<List<Valuation>>(valuationData);
                foreach (var valuation in valuations)
                {
                    context.Valuations.Add(valuation);
                }
                context.SaveChanges();
            }
        }

        public static void SeedOwnerType(PropertyDbContext context)
        {
            if (!context.OwnerTypes.Any())
            {
                var ownerTypeData = File.ReadAllText("./MockData/OwnerTypeSeedData.json");
                var ownerTypes = JsonConvert.DeserializeObject<List<OwnerType>>(ownerTypeData);
                foreach (var ownerType in ownerTypes)
                {
                    context.OwnerTypes.Add(ownerType);
                }
                context.SaveChanges();
            }
        }

        public static void SeedOwner(PropertyDbContext context)
        {
            if (!context.Owners.Any())
            {
                var ownerData = File.ReadAllText("./MockData/OwnerSeedData.json");
                var owners = JsonConvert.DeserializeObject<List<Owner>>(ownerData);
                foreach (var owner in owners)
                {
                    context.Owners.Add(owner);
                }
                context.SaveChanges();
            }
        }

        public static void SeedOwnershipLog(PropertyDbContext context)
        {
            if (!context.OwnershipLogs.Any())
            {
                var ownershipLogData = File.ReadAllText("./MockData/OwnershipLogSeedData.json");
                var ownershipLogs = JsonConvert.DeserializeObject<List<OwnershipLog>>(ownershipLogData);
                foreach (var ownershipLog in ownershipLogs)
                {
                    context.OwnershipLogs.Add(ownershipLog);
                }
                context.SaveChanges();
            }
        }

        public static void SeedAccountType(PropertyDbContext context)
        {
            if (!context.AccountTypes.Any())
            {
                var accountTypeData = File.ReadAllText("./MockData/AccountTypeSeedData.json");
                var accountTypes = JsonConvert.DeserializeObject<List<AccountType>>(accountTypeData);
                foreach (var accountType in accountTypes)
                {
                    context.AccountTypes.Add(accountType);
                }
                context.SaveChanges();
            }
        }

        public static void SeedAccount(PropertyDbContext context)
        {
            if (!context.Accounts.Any())
            {
                var accountData = File.ReadAllText("./MockData/AccountSeedData.json");
                var accounts = JsonConvert.DeserializeObject<List<Account>>(accountData);
                foreach (var account in accounts)
                {
                    context.Accounts.Add(account);
                }
                context.SaveChanges();
            }
        }
    }
}
