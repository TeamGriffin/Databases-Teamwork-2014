﻿namespace CarsFactory.Loaders
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml;

    using CarsFactory.Models;
    using CarsFactory.Data;

    public class XmlLoader
    {
        public static void LoadXmlFile(CarsFactoryContext context)
        {
            string fileName = @"..\..\..\Xml-Data-Load\Manufacturers-Expenses.xml";

            using (XmlReader reader = XmlReader.Create(fileName))
            {
                while (reader.Read())
                {
                    if ((reader.NodeType == XmlNodeType.Element) &&
                        (reader.Name == "manufacturer"))
                    {
                        var manafacturerName = reader.GetAttribute("name");
                        var manufacturerExists = context.Manufacturers.Any(m => m.Name == manafacturerName);

                        if (!manufacturerExists)
                        {
                            var manafacturer = new Manufacturer
                            {
                                Name = manafacturerName
                            };
                            context.Manufacturers.Add(manafacturer);
                            context.SaveChanges();
                        }

                        while (reader.Read())
                        {
                            if ((reader.NodeType == XmlNodeType.Element) &&
                                (reader.Name == "expenses"))
                            {
                                var monthId = int.Parse(reader.GetAttribute("month"));
                                var expenseeValue = decimal.Parse(reader.ReadElementString());

                                var monthExists = context.Months.Any(m => m.MonthId == monthId);
                                if (!monthExists)
                                {
                                    var month = new Month
                                    {
                                        MonthId = monthId,
                                    };

                                    context.Months.Add(month);
                                    context.SaveChanges();
                                }

                                var manafacturer = context.Manufacturers.First(m => m.Name == manafacturerName);

                                var expense = new Expense
                                {
                                    MonthId = monthId,
                                    ManafacturerId = manafacturer.Id,
                                    Value = expenseeValue
                                };

                                var expenseExists = context.Expenses.Any(e => e.ManafacturerId == manafacturer.Id);

                                if (!expenseExists)
                                {
                                    context.Expenses.Add(expense);
                                    context.SaveChanges();

                                    manafacturer.ExpenseId = expense.ExpenseId;
                                    context.SaveChanges();

                                }

                            }

                            else if ((reader.NodeType == XmlNodeType.EndElement) &&
                           (reader.Name == "manufacturer"))
                            {
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
