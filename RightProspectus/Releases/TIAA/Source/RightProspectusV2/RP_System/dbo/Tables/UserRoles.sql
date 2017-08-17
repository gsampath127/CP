CREATE TABLE [dbo].[UserRoles] (
    [UserId]          INT      NOT NULL,
    [RoleId]          INT      NOT NULL,
    [UtcModifiedDate] DATETIME DEFAULT (getutcdate()) NULL,
    [ModifiedBy]      INT      NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [fk_Roles] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[Roles] ([RoleId]),
    CONSTRAINT [fk_Users] FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);

