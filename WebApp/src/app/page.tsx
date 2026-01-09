export default function Home() {
  return (
    <div className="flex flex-col items-center justify-center min-h-screen px-6 py-12 text-center">
      <h1 className="text-3xl font-bold mb-6">Course Scoring Application</h1>
      <p className="max-w-2xl text-lg mb-8">
        This application provides a centralized way to view and manage course scoring data for competitive events. Users
        can explore individual events to review results, scoring details, and performance metrics, or browse athletes to
        see their accomplishments across events.
      </p>
      <p className="max-w-2xl text-lg mb-10">
        This tool can be use to look at an athlete's performance across multiple events, or to analyze results for a
        specific event. There are many ways which you can compare different athletes and results to get use useful
        information.
      </p>
      <p className="max-w-2xl text-lg mb-10">
        While the application is functional, the athlete and event names are synthetic — generated using the a .NET
        library to create realistic data at scale. Any resemblance to real people or events is purely coincidental.
      </p>
      <div className="flex gap-6">
        <a
          href="/races"
          className="px-6 py-3 rounded-md bg-blue-600 text-white font-medium hover:bg-blue-700 transition"
        >
          View Events
        </a>
        <a
          href="/athletes"
          className="px-6 py-3 rounded-md bg-gray-200 text-gray-800 font-medium hover:bg-gray-300 transition"
        >
          View Athletes
        </a>
      </div>
    </div>
  )
}
