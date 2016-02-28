using System.Data.Entity;

namespace SHWD.Platform.Process.Entities
{
    public class DevelopInitializer: DropCreateDatabaseIfModelChanges<ProcessContext>
    {
        protected override void Seed(ProcessContext context)
        {
            base.Seed(context);
        }
    }

    public class ProductionInitializer : CreateDatabaseIfNotExists<ProcessContext>
    {
        protected override void Seed(ProcessContext context)
        {
            base.Seed(context);
        }
    }
}
