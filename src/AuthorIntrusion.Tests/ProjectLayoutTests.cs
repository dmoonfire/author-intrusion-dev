namespace AuthorIntrusion.Tests
{
    using AuthorIntrusion.Buffers;

    using NUnit.Framework;

    /// <summary>
    /// Tests functionality for applying layouts and the resulting regions.
    /// </summary>
    [TestFixture]
    public class ProjectLayoutTests
    {
        /// <summary>
        /// Tests applying a single region from the default.
        /// </summary>
        [Test]
        public void ApplySingleLayout()
        {
            // Set up the layout.
            var projectLayout = new RegionLayout
                {
                    Name = "Project",
                    Slug = "project",
                    HasContent = false,
                };
            projectLayout.InnerLayouts.Add(
                new RegionLayout
                    {
                        Name = "Region 1",
                        Slug = "region-1",
                        HasContent = true,
                    });

            // Create a new project with the given layout.
            var project = new Project();
            project.ApplyLayout(projectLayout);

            // Assert the results.
            Assert.AreEqual(
                2, project.Regions.Count, "Number of regions is unexpected.");
            Assert.IsTrue(
                project.Regions.ContainsKey("project"),
                "Cannot find root project region.");
            Assert.IsTrue(
                project.Regions.ContainsKey("region-1"), "Cannot find region 1.");

            Assert.AreEqual(
                1,
                project.Regions["project"].Blocks.Count,
                "The root region has an unexpected number of blocks.");

            Assert.AreEqual(
                0,
                project.Regions["region-1"].Blocks.Count,
                "The region-1 region has an unexpected number of blocks.");
        }
    }
}