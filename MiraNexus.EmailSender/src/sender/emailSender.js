import dotenv from "dotenv";
import { createTransport } from "nodemailer";
import { MailtrapTransport } from "mailtrap";
import readFileActiveEmail from "../utils/readFile.js";

dotenv.config();

const {
  SMTP_USER,
  SMTP_PASS,
  SMTP_PORT,
  SMTP_HOST,
  FRONTEND_URL,
  PLATFORM_NAME,
  SUPPORT_PHONE,
  SUPPORT_EMAIL,
} = process.env;

function sendEmailActive(email, token, id) {
  const transport = createTransport({
    host: SMTP_HOST,
    port: SMTP_PORT,
    secure: true,
    auth: {
      user: SMTP_USER,
      pass: SMTP_PASS,
    },
  });

  const sender = { address: SMTP_USER, name: "Suporte da MiraNexus" };

  const html = readFileActiveEmail()
    .replaceAll("{{TOKEN}}", token)
    .replaceAll("{{CURRENT_YEAR}}", new Date().getFullYear())
    .replaceAll("{{PLATFORM_NAME}}", PLATFORM_NAME)
    .replaceAll("{{FRONTEND_URL}}", FRONTEND_URL)
    .replaceAll("{{SUPPORT_EMAIL}}", SUPPORT_EMAIL)
    .replaceAll("{{SUPPORT_PHONE}}", SUPPORT_PHONE)
    .replaceAll(
      "{{ACTIVATION_URL}}",
      `${FRONTEND_URL}/active/${id}?g=${token}&gn=${email}`
    );

  transport
    .sendMail({
      from: sender,
      to: email,
      subject: "Ative o seu cadastro",
      html: html,
      category: "Ativação de cadastro",
    })
    .then(({ accepted }) =>
      console.log(`Email enviado com sucesso para ${accepted}`)
    );
}

export const emailSenderService = {
  sendEmailActive,
};
