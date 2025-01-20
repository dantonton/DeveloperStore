﻿namespace DeveloperStore.Domain.Models
{
    public abstract class BaseModel
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
    }
}
    