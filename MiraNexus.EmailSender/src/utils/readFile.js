import fs from "fs";
import path from "path";
import { fileURLToPath } from "url";

const __filename = fileURLToPath(import.meta.url);
const __dirname = path.dirname(__filename);

export default function readFileActiveEmail() {
  try {
    const htmlPath = path.join(__dirname, "../views/active.email.html");
    const htmlContent = fs.readFileSync(htmlPath, "utf-8");
    return htmlContent;
  } catch (error) {
    console.error("Erro ao ler arquivo HTML:", error);
  }
}
