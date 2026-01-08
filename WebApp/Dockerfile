FROM node:24.12.0-alpine AS build
WORKDIR /app

RUN corepack enable

# Copy the files needed for this docker file.
COPY package.json pnpm-lock.yaml tsconfig.json ./
COPY src ./src
COPY public ./public

ENV CI=true

RUN pnpm install --frozen-lockfile
RUN pnpm build

FROM node:24.12.0-alpine AS runtime
WORKDIR /app

RUN corepack enable

# Copy built app and runtime deps
COPY --from=build /app/.next ./.next
COPY --from=build /app/public ./public
COPY --from=build /app/package.json ./package.json
COPY --from=build /app/node_modules ./node_modules

EXPOSE 3000

CMD ["pnpm", "start"]