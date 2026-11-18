import { RecordingPresets, useAudioPlayer } from "expo-audio";
import { Button, View, Text } from "react-native";

interface AudioPlayerProps {
    recordingUri : string | null;
}

export const AudioPlayer = ({recordingUri} : AudioPlayerProps) => {
    

    if(recordingUri == null){
        return <>
            <View>
                <Text>
                    No recording has been taken yet
                </Text>
            </View>
        </>
    }

    const file =  {uri: recordingUri}
    const player = useAudioPlayer(file);

        const handlePlay = () => {
        // Enable lock screen controls with metadata
        player.setActiveForLockScreen(true, {
            title: 'My Audio Title',
            artist: 'Artist Name',
            albumTitle: 'Album Name',
            artworkUrl: 'https://example.com/artwork.jpg', // optional
        });

        // Start playback - this will continue in the background
        player.play();
    };

    const handleStop = () => {
        player.pause();
        // Optionally disable lock screen controls when done
        player.setActiveForLockScreen(false);
    };


    return <>
        <View>
            <Button title="Play" onPress={handlePlay} />
            <Button title="Stop" onPress={handleStop} />
        </View>
    </>
}