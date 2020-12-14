using System;

namespace PrivateCloud.Infra.Sqlite.Dto
{
    public interface IDto
    {
        Guid Id { get; set; }

        int Flags { get; set; }
    }

    public enum DtoFlags
    {
        None = 0,
        Deleted = 1,
    }
}
