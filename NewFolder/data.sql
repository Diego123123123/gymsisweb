USE [siswebDB]
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'e024f56a-2f2e-4faf-9735-0b34b6cdfab8', N'pp@gmail.com', N'PP@GMAIL.COM', N'pp@gmail.com', N'PP@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAECfACPMDolYEFsHqVtuO3U1uHmp+Q9iEDsMg1EI18ijio8YIdo6s2BWKQPYJMqkIRQ==', N'FOPWG7RKXS2TDDAV6UKFLMCETQQWSURV', N'01555ba7-1c60-4739-96c9-1ccb9bac5c6e', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'ea9a9ecc-dd20-48f8-887b-23b9c44df472', N'admin@gmail.com', N'ADMIN@GMAIL.COM', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAEK1hcWTLAPNi/WytiExlycDh50iimAnxNDG7tt2ARjL7P+2Jj8VESM93iUT9IFJMjw==', N'4SRJ4UZ6KSYGCSYHMU24WRNN75ML4F3X', N'4085efbe-c787-4165-bf46-608ace064024', NULL, 0, 0, NULL, 1, 0)
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190524192827_initial', N'2.1.8-servicing-32085')
GO
SET IDENTITY_INSERT [dbo].[Functions] ON 
GO
INSERT [dbo].[Functions] ([FunctionId], [MovieName], [Schedule], [Room]) VALUES (1, N'shrek', N'9:45', 5)
GO
INSERT [dbo].[Functions] ([FunctionId], [MovieName], [Schedule], [Room]) VALUES (2, N'avengers', N'9:45', 5)
GO
INSERT [dbo].[Functions] ([FunctionId], [MovieName], [Schedule], [Room]) VALUES (3, N'pinocho', N'10:15', 1)
GO
SET IDENTITY_INSERT [dbo].[Functions] OFF
GO
SET IDENTITY_INSERT [dbo].[Reserves] ON 
GO
INSERT [dbo].[Reserves] ([ReserveId], [User], [AmountOfPeople], [FunctionId]) VALUES (6, N'e024f56a-2f2e-4faf-9735-0b34b6cdfab8', 80, 1)
GO
SET IDENTITY_INSERT [dbo].[Reserves] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserId], [Name], [LastName], [Email], [Password]) VALUES (1, N'Pepe', N'Pica', N'pp@gmail.com', N'aA123456:')
GO
INSERT [dbo].[Users] ([UserId], [Name], [LastName], [Email], [Password]) VALUES (2, N'Admin', N'Admin', N'admin@gmail.com', N'Aa123456:')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
