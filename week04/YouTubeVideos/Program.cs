namespace YouTubeVideos
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create videos
            Video video1 = new Video("How to Cook Jollof Rice", "Chef Anita", 600);
            Video video2 = new Video("C# OOP Basics", "TechWithTim", 1200);
            Video video3 = new Video("Funny Cat Compilation", "AnimalHub", 300);

            // Add comments
            video1.AddComment(new Comment("John", "This looks so delicious!"));
            video1.AddComment(new Comment("Mary", "Thanks for the recipe."));
            video1.AddComment(new Comment("Paul", "Tried it today and it was great."));

            video2.AddComment(new Comment("Alice", "Very clear explanation."));
            video2.AddComment(new Comment("Bob", "Can you make one on interfaces?"));
            video2.AddComment(new Comment("Charlie", "Helped me with my homework."));

            video3.AddComment(new Comment("Lucy", "Hahaha, I can't stop laughing!"));
            video3.AddComment(new Comment("David", "Cats are the best."));
            video3.AddComment(new Comment("Emma", "I showed this to my kids."));

            // Store in list
            List<Video> videos = new List<Video> { video1, video2, video3 };

            // Display
            foreach (var video in videos)
            {
                video.Display();
            }
        }
    }
}
