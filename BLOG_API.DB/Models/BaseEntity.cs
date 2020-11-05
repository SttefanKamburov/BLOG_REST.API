using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BLOG_API.DB.Models
{
    public abstract class BaseEntity
    {
        public virtual long Id { get; set; }
        public bool IsDeleted { get; set; }
        public virtual DateTime DataCreated { get; set; }
        public virtual DateTime LastDateModified { get; set; }
        public virtual User UserCreator { get; set; }      

        [ForeignKey(nameof(UserCreator))]
        public virtual long? UserCreatorId { get; set; }
        public virtual User LastUserModifier { get; set; }

        [ForeignKey(nameof(LastUserModifier))]
        public virtual long? LastUserModifierId { get; set; }
    }
}
