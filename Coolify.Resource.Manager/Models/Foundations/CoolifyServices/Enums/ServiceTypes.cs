// ---------------------------------------------------------------
// Copyright (c) Coolify.Resource.Manager Contributors
// FREE TO USE TO CONNECT THE WORLD
// ---------------------------------------------------------------
// ---------------------------------------------------------------
// Static constant bag for all Coolify one-click service type strings.
// The API accepts these as the "type" field on POST /api/v1/services.
// When type is omitted, DockerComposeRaw is used instead (custom stack).
// ---------------------------------------------------------------

namespace Coolify.Resource.Manager.Models.Foundations.CoolifyServices.Enums
{
    public static class ServiceTypes
    {
        // ---- Messaging ----
        public const string RabbitMq = "rabbitmq";

        // ---- Version Control & CI/CD ----
        public const string Gitea = "gitea";
        public const string GiteaWithMySql = "gitea-with-mysql";
        public const string Jenkins = "jenkins";
        public const string Onedev = "onedev";
        public const string SonatypeNexus = "sonatype-nexus";

        // ---- Databases & Data ----
        public const string Supabase = "supabase";
        public const string RedisInsight = "redis-insight";
        public const string NeonWsProxy = "neon-ws-proxy";
        public const string Sequin = "sequin";
        public const string Databasus = "databasus";

        // ---- CMS ----
        public const string Directus = "directus";
        public const string Ghost = "ghost";
        public const string WordPress = "wordpress";
        public const string Strapi = "strapi";
        public const string Docmost = "docmost";
        public const string DokuWiki = "dokuwiki";

        // ---- Analytics & Monitoring ----
        public const string Grafana = "grafana";
        public const string Umami = "umami";
        public const string PostHog = "posthog";
        public const string Plausible = "plausible";
        public const string Swetrix = "swetrix";
        public const string Superset = "superset";

        // ---- Automation & Workflow ----
        public const string N8N = "n8n";
        public const string ActivePieces = "activepieces";
        public const string Trigger = "trigger";
        public const string Prefect = "prefect";
        public const string MageAi = "mage-ai";

        // ---- Low-code & No-code ----
        public const string NocoDB = "nocodb";
        public const string Appsmith = "appsmith";
        public const string Lowcoder = "lowcoder";
        public const string Tooljet = "tooljet";
        public const string NocoBase = "nocobase";

        // ---- AI & ML ----
        public const string AnythingLlm = "anythingllm";
        public const string Flowise = "flowise";
        public const string Chroma = "chroma";
        public const string Qdrant = "qdrant";
        public const string Argilla = "argilla";
        public const string Unstructured = "unstructured";

        // ---- Storage & Files ----
        public const string Minio = "minio";
        public const string Filebrowser = "filebrowser";
        public const string Duplicati = "duplicati";

        // ---- Communication & Messaging ----
        public const string RocketChat = "rocket.chat";
        public const string Mattermost = "mattermost";
        public const string Matrix = "matrix";
        public const string Soketi = "soketi";
        public const string SoketiAppManager = "soketi-app-manager";
        public const string EvolutionApi = "evolution-api";

        // ---- Email ----
        public const string Mailpit = "mailpit";

        // ---- Project Management & Productivity ----
        public const string Fider = "fider";
        public const string Twenty = "twenty";
        public const string Redmine = "redmine";
        public const string Rallly = "rallly";
        public const string Slash = "slash";
        public const string Organizr = "organizr";

        // ---- Dashboards ----
        public const string Homepage = "homepage";
        public const string Homarr = "homarr";
        public const string Dashy = "dashy";
        public const string Glance = "glance";
        public const string Heimdall = "heimdall";

        // ---- Media ----
        public const string Emby = "emby";
        public const string EmbyStat = "emby-stat";
        public const string Radarr = "radarr";

        // ---- Dev Tools ----
        public const string Dozzle = "dozzle";
        public const string Gotenberg = "gotenberg";
        public const string DockerRegistry = "docker-registry";
        public const string Docuseal = "docuseal";
        public const string StirlingPdf = "stirling-pdf";
        public const string ItTools = "it-tools";
        public const string Hoppscotch = "hoppscotch";
        public const string JupyterNotebook = "jupyter-notebook";
        public const string Marimo = "marimo";
        public const string Martin = "martin";
        public const string Kuzzle = "kuzzle";
        public const string Syncthing = "syncthing";
        public const string Snapdrop = "snapdrop";

        // ---- Authentication & Security ----
        public const string Authentik = "authentik";
        public const string Keycloak = "keycloak";
        public const string Unleash = "unleash";

        // ---- Finance ----
        public const string FireflyIii = "firefly-iii";
        public const string ActualBudget = "actualbudget";

        // ---- Localization & Feedback ----
        public const string Tolgee = "tolgee";

        // ---- Status & Uptime ----
        public const string StatusNook = "statusnook";

        // ---- Social & Blogging ----
        public const string Fediverse = "fediverse";

        // ---- Website Builders ----
        public const string Nitropage = "nitropage";
        public const string Vvveb = "vvveb";

        // ---- Misc ----
        public const string CalibreeWeb = "calibre-web";
        public const string Searxng = "searxng";
        public const string Shlink = "shlink";
        public const string TrailBase = "trailbase";
        public const string RivetEngine = "rivet-engine";
        public const string NextImageTransformation = "next-image-transformation";
        public const string Wallabag = "wallabag";
        public const string ReactiveResume = "reactive-resume";
    }
}
