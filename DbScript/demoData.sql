INSERT [dbo].[Countries] ([Id], [Name]) VALUES (1, N'Egypt')
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (2, N'Saudi Arabia')


INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (1, N'Cairo', 1)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (2, N'Alexandria ', 1)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (3, N'Riyadh', 2)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (4, N'Dammam', 2)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (5, N'Jeddah', 2)
INSERT [dbo].[Cities] ([Id], [Name], [CountryId]) VALUES (7, N'Fayoum', 1)


INSERT [dbo].[Employees] ([Id], [Name], [Email], [Salary], [CityId]) VALUES (1, N'Ahmed', N'Ahmed@email.com', 500, 4)
INSERT [dbo].[Employees] ([Id], [Name], [Email], [Salary], [CityId]) VALUES (6, N'Mahmoud', N'Mahmoud@Email.com', 574, 1)
INSERT [dbo].[Employees] ([Id], [Name], [Email], [Salary], [CityId]) VALUES (9, N'Mostafa', N'Mostafa@email.com', 123, 4)
