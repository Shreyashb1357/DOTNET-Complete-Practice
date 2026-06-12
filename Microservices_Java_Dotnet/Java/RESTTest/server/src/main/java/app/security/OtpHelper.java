package app.security;

import java.security.SecureRandom;
import java.util.Collections;
import java.util.HashMap;
import java.util.Map;
import java.util.Properties;

import jakarta.mail.MessagingException;
import jakarta.mail.Session;
import jakarta.mail.Transport;
import jakarta.mail.Message.RecipientType;
import jakarta.mail.internet.MimeMessage;

public class OtpHelper {

    private static final Map<String, Integer> store = Collections.synchronizedMap(new HashMap<>());

    public static void mailPasscode(String receiver, String sender) {
        Integer passcode = new SecureRandom().nextInt(100000, 1000000);
        var settings = new Properties();
        settings.put("mail.smtp.host", "localhost");
        settings.put("mail.smtp.port", "8025");
        var session = Session.getInstance(settings);
        var message = new MimeMessage(session);
        try{
            message.setFrom(sender);
            message.setRecipients(RecipientType.TO, receiver);
            message.setSubject("OTP");
            message.setContent(passcode.toString(), "text/plain");
            Transport.send(message);
        }catch(MessagingException e){
            throw new RuntimeException(e);
        }
        store.put(receiver, passcode);
    }

    public static boolean verifyPasscode(String receiver, Integer passcode) {
        Integer value = store.remove(receiver);
        return passcode.equals(value);
    }
}
