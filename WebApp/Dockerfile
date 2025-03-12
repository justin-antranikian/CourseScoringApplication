# Use Node.js 23.3.0 for both build and runtime
FROM node:23.3.0-alpine AS builder

# Set the working directory
WORKDIR /app

# Install dependencies
COPY package.json package-lock.json ./
RUN npm ci --production=false

# Copy source code and build the Next.js app
COPY . .
RUN npm run build

# Use a lighter Node.js image for runtime
FROM node:23.3.0-alpine AS runner

WORKDIR /app

# Copy only the necessary files from the builder
COPY --from=builder /app/package.json ./
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/public ./public

# Set environment variables
ENV NODE_ENV=production

# Expose the port Next.js uses
EXPOSE 3000

# Start the Next.js app
CMD ["node", ".next/standalone/server.js"]