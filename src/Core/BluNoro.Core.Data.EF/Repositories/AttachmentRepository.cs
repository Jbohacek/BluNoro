using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BluNoro.Core.Common.Entities;
using BluNoro.Core.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace BluNoro.Core.Data.EF.Repositories
{
    public class AttachmentRepository(DbContext context) : Repo<Attachment>(context);
}
