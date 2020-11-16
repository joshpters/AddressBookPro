using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AddressBookPro.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AddressBookPro.Data
{
    public static class ContextSeed
    {
        private static string[] labelNames =
        {
            "No Label",
            "Friend",
            "Acquaintance",
            "Coworker",
        };
        //seed users
        public static async Task SeedLabels(ApplicationDbContext context)
        {
            foreach (var labelName in labelNames)
            {
                try
                {
                    if ((await context.Labels.FirstOrDefaultAsync(l => l.Name == labelName)) == null)
                    {
                        Label label = new Label();
                        label.Name = labelName;
                        context.Add(label);
                        await context.SaveChangesAsync();
                    }
                }
                catch
                {
                    throw;
                }
            }
        }
    }
}