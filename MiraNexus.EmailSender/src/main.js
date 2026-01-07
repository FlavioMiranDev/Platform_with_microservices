import dotenv from "dotenv";
import { connect } from "amqplib";
import { emailSenderService } from "./sender/emailSender.js";

dotenv.config();

const {
  RABBITMQ_HOST,
  RABBITMQ_PORT,
  RABBITMQ_USER,
  RABBITMQ_PASS,
  QUEUE_NAME,
} = process.env;

async function start() {
  console.log(
    RABBITMQ_HOST,
    RABBITMQ_PORT,
    RABBITMQ_USER,
    RABBITMQ_PASS,
    QUEUE_NAME
  );

  const connection = await connect({
    protocol: "amqp",
    hostname: RABBITMQ_HOST,
    port: Number(RABBITMQ_PORT),
    username: RABBITMQ_USER,
    password: RABBITMQ_PASS,
    vhost: "/",
  });

  const channel = await connection.createChannel();

  await channel.assertQueue(QUEUE_NAME, {
    durable: true,
  });

  channel.prefetch(5);

  channel.consume(
    QUEUE_NAME,
    async (msg) => {
      if (!msg) return;
      try {
        const response = JSON.parse(msg.content.toString());
        const { id, email, token } = response.message;

        emailSenderService.sendEmailActive(email, token, id);

        channel.ack(msg);
      } catch {
        channel.nack(msg, false, true);
      }
    },
    { noAck: false }
  );
}

start();
