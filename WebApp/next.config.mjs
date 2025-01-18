/** @type {import('next').NextConfig} */
const nextConfig = {
  async redirects() {
    return [
      {
        source: "/",
        destination: "/races",
        permanent: false,
      },
    ]
  },
}

export default nextConfig
