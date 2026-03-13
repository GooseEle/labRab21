namespace labRab21.Domain
{
    internal class Teacher
    {
        private DistanceLearningService distanceLearningService;

        public Teacher()
        {

        }

        public Teacher(string v, DistanceLearningService distanceLearningService, Institute institute)
        {
            this.FullName = v;
            this.Service = distanceLearningService;
            Institute = institute;
        }

        public string FullName { get; set; }
        public Institute Institute { get; set; }

        public DistanceLearningService Service { get; set; }
    }
}
